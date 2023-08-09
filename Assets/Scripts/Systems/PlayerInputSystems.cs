using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Mathematics;
using UnityEngine;



/// <summary>
/// Système mettant à jour la position du joueur
/// </summary>
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        MovePlayerJob job = new MovePlayerJob
        {
            DeltaTime = deltaTime,
        };

        job.Run();
    }

}

/// <summary>
/// Système permettant de faire sauter le joueur
/// </summary>
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerJumpSystem : ISystem
{
    /// <summary>
    /// Faire sauter le joueur lorsque l'événement PlayerJumpEvent est actif
    /// </summary>
    /// <param name="state"></param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // parfois le joueur est propulsé dans la stratosphère ceci est un hotfix pour limiter sa vélocité verticale
        foreach (var (velocity,jumpforce, _) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<JumpForceComponent>, RefRO<IsPlayerTag>>())
        {
            if (velocity.ValueRO.Linear.y > jumpforce.ValueRO.Force)
            {
                velocity.ValueRW.Linear.y = jumpforce.ValueRO.Force;
            }
        }
        

        foreach (var (velocity,mass,transform,jumpForce, _) in SystemAPI.Query<
            RefRW<PhysicsVelocity>,
            RefRO<PhysicsMass>,
            RefRO<LocalTransform>,
            RefRO<JumpForceComponent>,
            RefRO<PlayerJumpEvent>
            >())
        {
            var trans = transform.ValueRO;
            if (velocity.ValueRO.Linear.y <= 0.1) // On veux permettre au joueur de sauter 
            {                                     // lorsqu'il est en train de retomber
                velocity.ValueRW.ApplyImpulse(mass.ValueRO, trans.Position, trans.Rotation, new(0, jumpForce.ValueRO.Force * 100, 0), float3.zero);
            }
            Entity player = SystemAPI.GetSingletonEntity<IsPlayerTag>();
            SystemAPI.SetComponentEnabled<PlayerJumpEvent>(player, false);
        }
    }

}

/// <summary>
/// Système permettant de sélectionner une entité à l'aide de la souris
/// </summary>
public partial struct PlayerMouseInteractionSystem : ISystem
{
    /// <summary>
    /// Mets à jour l'entité sélectionnée de MouseClickEvent à chaque frame.
    /// </summary>
    /// <param name="state"></param>
    public void OnUpdate(ref SystemState state)
    {
        MouseInputComponent mouseInput = SystemAPI.GetSingleton<MouseInputComponent>();

        Camera camera = CameraSingleton.Instance.GetComponent<Camera>();
        float3 origin = new(mouseInput.Value, camera.nearClipPlane);
        
        var ray = camera.ScreenPointToRay(origin);

        var raycastInput = new RaycastInput
        {
            Start = ray.origin,
            End = ray.GetPoint(camera.farClipPlane),
            Filter = new CollisionFilter 
            {
                BelongsTo = 1,
                CollidesWith = 1,
                GroupIndex = 0,
            }
        };

        var collector = new ClosestHitCollector<Unity.Physics.RaycastHit>(1.0f);

        PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

        if (physicsWorldSingleton.CastRay(raycastInput, ref collector))
        {

            foreach(var mouseClickEvent in SystemAPI.Query<RefRW<MouseClickEvent>>())
            {
                mouseClickEvent.ValueRW.SelectedEntity = collector.ClosestHit.Entity;
            }
        }
    }
}

/// <summary>
/// Système permettant de cliquer avec la souris
/// </summary>
public partial struct MouseClickSystem : ISystem
{
    /// <summary>
    /// Éxécute l'événement MouseClickEvent lorsque qu'il est actif et effectu l'action approprié
    /// </summary>
    /// <param name="state"></param>
    public void OnUpdate(ref SystemState state)
    {
        foreach (var mouseClick in SystemAPI.Query<RefRO<MouseClickEvent>>())
        {
            switch (Inventory.Instance.selectedSlot)
            {
                case 0: // Marteau Magigque
                    // Briser un block
                    var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
                    var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

                    var blockToBreak = mouseClick.ValueRO.SelectedEntity;

                    if (blockToBreak != null)
                    {
                        ecb.AddComponent(blockToBreak, new ComponentType(typeof(Disabled)));
                    }
                    break;

                case 1: // Sceptre Magique
                    // Attaquer
                    float2 mousePos = SystemAPI.GetSingleton<MouseInputComponent>().Value;

                    var playerEntity = SystemAPI.GetSingletonEntity<IsPlayerTag>();
                    var playerPos = SystemAPI.GetComponentRO<LocalTransform>(playerEntity).ValueRO.Position;

                    // Convertir la position du joueur en position sur l'écran pour pouvoir comparer avec la souris
                    float3 playerScreenPos = CameraSingleton.Instance.GetComponent<Camera>().WorldToScreenPoint(playerPos);


                    var projectile = SystemAPI.GetComponent<PrefabComponent>(playerEntity);

                    var entity = state.EntityManager.Instantiate(projectile.Prefab);

                    Vector2 direction = playerScreenPos.xy - mousePos;
                    direction.Normalize();

                    state.EntityManager.SetComponentData(entity, LocalTransform.FromPosition(new(playerPos.x - direction.x, playerPos.y - direction.y, 0)));
                    state.EntityManager.SetComponentData(entity, new DirectionComponent { Value = direction });

                    break;
                default: // emplacement vide et chat de la sorcière
                    // Ne rien faire
                    break;
            }

            // Désactiver l'événement
            Entity player = SystemAPI.GetSingletonEntity<IsPlayerTag>();
            SystemAPI.SetComponentEnabled<MouseClickEvent>(player, false);

        }
    }
}
