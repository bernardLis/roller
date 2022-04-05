using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectibleUI : MonoBehaviour
{
    CollectibleManager _collectibleManager;

    Label _collectibleCountLabel;
    int _collectibleCount = 0;


    VisualElement _collectiblesMenuContainer;
    VisualElement _collectibleContainer;


    void Awake()
    {
        _collectibleManager = CollectibleManager.Instance;

        var root = GetComponent<UIDocument>().rootVisualElement;

        _collectibleCountLabel = root.Q<Label>("collectibleCount");
        _collectibleCountLabel.text = _collectibleCount + "/" + _collectibleManager.collectibles.Count;

        _collectiblesMenuContainer = root.Q<VisualElement>("collectiblesMenuContainer");
        _collectibleContainer = root.Q<VisualElement>("collectibleContainer");
    }

    public void AddToCollected()
    {
        _collectibleCount++;
        _collectibleCountLabel.text = _collectibleCount + "/" + _collectibleManager.collectibles.Count;
    }

    public void ToggleCollectiblesMenu()
    {
        if (_collectiblesMenuContainer.style.display == DisplayStyle.None)
        {
            _collectiblesMenuContainer.style.display = DisplayStyle.Flex;
            DisplayCollectibles();
        }
        else
        {
            _collectiblesMenuContainer.style.display = DisplayStyle.None;
            _collectibleContainer.Clear();
        }
    }

    void DisplayCollectibles()
    {
        foreach (Collectible c in _collectibleManager.collectibles)
        {
            CollectibleVisualElement v = new CollectibleVisualElement();
            v.Initialize(c);
            _collectibleContainer.Add(v);
        }
        // TODO: ask on forum how to do it properly.
        VisualElement ucc = _collectibleContainer.Q<VisualElement>("unity-content-container");
        ucc.style.flexWrap = Wrap.Wrap;
        ucc.style.flexDirection = FlexDirection.Row;
    }
}
