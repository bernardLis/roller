using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> Floors = new();
    public Transform FloorHolder;
    public GameObject FloorPrefab;
    public float Speed = 10.0f;

    public string SpawnTag;

    Label _collectibleCountLabel;
    int _collectibleCount = 0;

    VisualElement _collectiblesMenuContainer;

    Rigidbody _rb;
    CollectibleManager _collectibleManager;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        _collectibleCountLabel = root.Q<Label>("collectibleCount");
        _collectibleCountLabel.text = "0";

        _collectiblesMenuContainer = root.Q<VisualElement>("collectiblesMenuContainer");
        _collectibleManager = GetComponent<CollectibleManager>();
    }

    void Start()
    {
        Vector3 floorPos = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);
        SpawnFloor(floorPos);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("c"))
            ToggleCollectiblesMenu();

        //        if ( != 0)
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.fixedDeltaTime * Speed);

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.fixedDeltaTime * Speed * 10, Space.World);
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.transform.CompareTag(SpawnTag))
            HandleFloorSpawning(_col);

        if (_col.transform.CompareTag("Collectible"))
            HandleCollectible(_col);
    }

    void HandleFloorSpawning(Collider _col)
    {
        _col.transform.gameObject.SetActive(false); // don't spawn many floors

        float x = _col.transform.parent.transform.position.x;
        float y = _col.transform.parent.transform.position.y;
        float z = _col.transform.parent.transform.position.z;

        Vector2 disableColliderPosition = Vector2.zero;
        Vector3 pos = new Vector3(x, y, z);


        ColliderDirection colDir = _col.transform.GetComponent<FloorCollider>().colDirection;
        // normal
        if (colDir == ColliderDirection.NegativeX)
            pos.z += 20f;
        if (colDir == ColliderDirection.PositiveX)
            pos.z -= 20f;
        if (colDir == ColliderDirection.NegativeZ)
            pos.x -= 20f;
        if (colDir == ColliderDirection.PositiveZ)
            pos.x += 20f;

        // corners
        if (colDir == ColliderDirection.CornerNegXNegZ)
        {
            pos.z += 20f;
            pos.x -= 20f;
        }
        if (colDir == ColliderDirection.CornerPosXNegZ)
        {
            pos.z -= 20f;
            pos.x -= 20f;
        }
        if (colDir == ColliderDirection.CornerNegXPosZ)
        {
            pos.z += 20f;
            pos.x += 20f;
        }
        if (colDir == ColliderDirection.CornerPosXPosZ)
        {
            pos.z -= 20f;
            pos.x += 20f;
        }

        SpawnFloor(pos);
    }

    void SpawnFloor(Vector3 _pos)
    {
        GameObject f = Instantiate(FloorPrefab, _pos, Quaternion.Euler(-90, 0, 0));
        Floors.Add(f);
        f.GetComponent<Floor>().Initialize(_collectibleManager.GetRandomCollectible());
        f.transform.parent = FloorHolder;
    }

    // TODO: maybe this should be done by the collectibles?
    void HandleCollectible(Collider _col)
    {
        _collectibleCount++;
        _collectibleCountLabel.text = _collectibleCount.ToString();
        _col.gameObject.GetComponent<CollectibleGameObject>().collectible.collected = true;
        Destroy(_col.transform.gameObject);
    }

    void ToggleCollectiblesMenu()
    {
        Debug.Log("toggling collectibles menu");

        // TODO:
        // I'd like to pause the game on opening the menu and resume on closing. 
        // fill the menu with collectibles and mark the collected ones

        if (_collectiblesMenuContainer.style.display == DisplayStyle.None)
        {
            _collectiblesMenuContainer.style.display = DisplayStyle.Flex;
            DisplayCollectibles();
        }
        else
        {
            _collectiblesMenuContainer.style.display = DisplayStyle.None;
            _collectiblesMenuContainer.Clear();
        }
    }

    void DisplayCollectibles()
    {
        foreach (Collectible c in _collectibleManager.collectibles)
        {
            VisualElement v = new VisualElement();
            v.style.backgroundImage = c.icon.texture;
            if (c.collected)
                v.AddToClassList("collected");
            else
                v.AddToClassList("uncollected");

            _collectiblesMenuContainer.Add(v);
        }
    }
}
