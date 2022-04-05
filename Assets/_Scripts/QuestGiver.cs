using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    Collectible _collectible;
    QuestItemDisplay _questItemDisplay;

    public void Initialize(Collectible collectible)
    {

        _collectible = collectible;
        _questItemDisplay = GetComponentInChildren<QuestItemDisplay>();
        _questItemDisplay.Initialize(collectible);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name}");

        if (_collectible.collected)
            Debug.Log($"Congratz player, you have found the card.");
        else
            Debug.Log($"Keep looking.");



    }

}
