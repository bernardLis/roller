using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CollectibleManager : MonoBehaviour
{
    public List<Collectible> collectibles = new();
    void Awake()
    {
        LoadCollectibles();
    }

    void LoadCollectibles()
    {
        object[] loadedCollectibles = Resources.LoadAll("Collectibles", typeof(Collectible));
        for (int x = 0; x < loadedCollectibles.Length; x++)
        {
            collectibles.Add((Collectible)loadedCollectibles[x]);
        }
    }

    public Collectible GetRandomCollectible()
    {
        // TODO: return only not collected collectibles... easy?
        if (collectibles.Count == 0)
            return null;
        return collectibles[Random.Range(0, collectibles.Count)];
    }

}
