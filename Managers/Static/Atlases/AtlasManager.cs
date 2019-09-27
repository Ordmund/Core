using System;
using Core.Managers;
using UnityEngine;

public class AtlasManager : IDistributable //TODO ????????
{
    public static Sprite GetSprite(string spriteName)
    {
        throw new NotImplementedException();
    }

    private static void LoadAtlas()
    {

    }

    public int InitializationGeneration { get; }
    public void Initialize(Distributor distributor)
    {
        throw new NotImplementedException();
    }

    public void Restart(Distributor distributor)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}