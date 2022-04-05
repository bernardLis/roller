using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CollectibleManager : MonoBehaviour
{
    public List<Collectible> collectibles = new();

    public static CollectibleManager Instance { get; private set; }
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        LoadCollectibles();
    }

    void LoadCollectibles()
    {
        object[] loadedCollectibles = Resources.LoadAll("Collectibles", typeof(Collectible));
        for (int x = 0; x < loadedCollectibles.Length; x++)
        {
            collectibles.Add((Collectible)loadedCollectibles[x]);
        }

        // TODO: not needed in build
        foreach (Collectible c in collectibles)
        {
            c.collected = false;
        }
    }

    public Collectible GetRandomCollectible()
    {
        if (collectibles.Count == 0)
            return null;
        return collectibles[Random.Range(0, collectibles.Count)];
    }

    public Collectible GetRandomUncollectedCollectible()
    {
        List<Collectible> uncollected = new();
        foreach (Collectible c in collectibles)
            if (c.collected == false)
                uncollected.Add(c);
        
        if(uncollected.Count == 0)
            return null;
        
        return uncollected[Random.Range(0, collectibles.Count)];
    }


}
