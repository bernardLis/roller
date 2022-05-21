using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.VFX;

public class PlayerController : LivingEntity
{
    public List<GameObject> Floors = new();
    public Transform FloorHolder;
    public GameObject FloorPrefab;
    public float Speed = 10.0f;

    [SerializeField] string _spawnTag;

    Rigidbody _rb;
    CollectibleManager _collectibleManager;
    CollectibleUI _collectibleUI;

    [SerializeField] GameObject _particleCollectionEffect;
    [SerializeField] GameObject _bigSphere;
    [SerializeField] GameObject _effectSpawnPoint;
    [SerializeField] GameObject _electricDischarge;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collectibleManager = CollectibleManager.Instance;
        _collectibleUI = GetComponent<CollectibleUI>();
    }

    protected override void Start()
    {
        base.Start();
        startingHealth = 10000;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
            StartCoroutine(ElectricDischarge());
        if (Input.GetKeyDown("c"))
            _collectibleUI.ToggleCollectiblesMenu();
        if (Input.GetKeyDown("f"))
            ShakeSphere();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.fixedDeltaTime * Speed);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.fixedDeltaTime * Speed * 10, Space.World);
    }

    IEnumerator ElectricDischarge()
    {
        Debug.Log("click");
        GameObject electricty = Instantiate(_electricDischarge,
                                _effectSpawnPoint.transform.position, Quaternion.identity);
        electricty.transform.parent = transform;
        electricty.transform.localRotation = Quaternion.identity;
        VisualEffect effect = electricty.GetComponentInChildren<VisualEffect>();
        yield return new WaitForSeconds(1f);

        float endTime = Time.time + 1f;
        float baseRadius = 0.01f;
        while (Time.time < endTime)
        {
            Debug.Log("in while");
            baseRadius += 0.02f;
            effect.SetFloat("Cone_baseRadius", baseRadius);
            yield return null;
        }
        // 


        yield return new WaitForSeconds(0.5f);


        Destroy(electricty);

        //yield return null;
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.transform.CompareTag(_spawnTag))
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
        f.GetComponent<Floor>().Initialize();
        f.transform.parent = FloorHolder;
    }


    public override void Die()
    {
        Debug.Log("oh player you are dead");
    }

    // TODO: maybe this should be done by the collectibles?
    void HandleCollectible(Collider _col)
    {
        _collectibleUI.AddToCollected();
        _col.gameObject.GetComponent<CollectibleGameObject>().Collected();
    }

    void ShakeSphere()
    {
        _bigSphere.transform.DOShakePosition(1, 10, 10, 90);
    }

}
