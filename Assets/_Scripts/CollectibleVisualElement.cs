using UnityEngine.UIElements;

public class CollectibleVisualElement : VisualElement
{
    Collectible _collectible;

    public void Initialize(Collectible c)
    {
        _collectible = c;

        style.backgroundImage = c.icon.texture;
        if (c.collected)
            AddToClassList("collected");
        else
            AddToClassList("uncollected");

    }
}
