using UnityEngine;

public class Collectible : BaseScriptableObject
{
    public Sprite icon;
    public bool collected;

    public void Create(Sprite _icon)
    {
        icon = _icon;
    }
}
