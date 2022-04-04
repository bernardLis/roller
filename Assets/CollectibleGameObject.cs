using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGameObject : MonoBehaviour
{
    public Collectible collectible;

    public void Initialize(Collectible _collectible)
    {
        collectible = _collectible;


        foreach (Transform c in transform)
        {
            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr == null)
                continue;

            sr.sprite = _collectible.icon;
        }

    }

}
