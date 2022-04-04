using UnityEngine;
using DG.Tweening;

public class CollectibleGameObject : MonoBehaviour
{
    public Collectible _collectible;
    [SerializeField] Light _light;
    [SerializeField] GameObject _particleEffect;


    public void Initialize(Collectible collectible, Color col)
    {
        _collectible = collectible;

        foreach (Transform c in transform)
        {
            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr == null)
                continue;

            sr.sprite = collectible.icon;
        }

        GetComponent<Renderer>().material.color = col;
        _light.color = col;

        // start turning
        Vector3 rotation = new Vector3(0, 180, 0);
        transform.DOLocalRotate(rotation, 4).SetLoops(-1, LoopType.Incremental);
    }

    public void Collected()
    {
        _collectible.collected = true;
        DOTween.KillAll();
        Vector3 rotation = new Vector3(-90, 0, 0);
        GameObject effect = Instantiate(_particleEffect, transform.position, Quaternion.Euler(rotation));
        Destroy(effect, 1);
        Destroy(gameObject);
    }
}