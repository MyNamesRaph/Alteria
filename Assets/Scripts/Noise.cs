using System;
/// <summary>
/// Singleton permettant d'acc�der a une instance de FastNoiseLite depuis un syst�me
/// </summary>
public class Noise
{
    private static readonly Lazy<Noise> lazy = new Lazy<Noise>(() => new Noise());

    public static Noise Instance { get { return lazy.Value; } }
    public FastNoiseLite FastNoiseLite { get; private set; }

    private Noise() {}

    public void SetSeed(int seed)
    {
        FastNoiseLite = new FastNoiseLite(seed);
    }



}
