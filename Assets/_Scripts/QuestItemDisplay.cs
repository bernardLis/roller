using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class QuestItemDisplay : MonoBehaviour
{
    public void Initialize(Collectible collectible)
    {
        foreach (Transform c in transform)
        {
            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr == null)
                continue;

            sr.sprite = collectible.icon;
        }

        // start turning
        Vector3 rotation = new Vector3(0, 180, 0);
        transform.DOLocalRotate(rotation, 4).SetLoops(-1, LoopType.Incremental);
    }
}
