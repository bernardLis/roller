using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject collectiblePrefab;

    [Header("Colliders")]
    public GameObject positiveZCollider;
    public GameObject negativeZCollider;
    public GameObject positiveXCollider;
    public GameObject negativeXCollider;

    public GameObject cornerNegXNegZ;
    public GameObject cornerPosXNegZ;
    public GameObject cornerNegXPosZ;
    public GameObject cornerPosXPosZ;

    public void Initialize(Collectible _collectible)
    {
        DisableSpawnColliders(false);

        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        GetComponent<Renderer>().material.color = new Color(r, g, b);

        GameObject collectibleGO = Instantiate(collectiblePrefab, Vector3.zero, Quaternion.identity);
        float xRand = transform.position.x + Random.Range(-5f, 5f);
        float zRand = transform.position.z + Random.Range(-5f, 5f);
        collectibleGO.transform.position = new Vector3(xRand, transform.position.y + 1, zRand); // annoying
        collectibleGO.GetComponent<CollectibleGameObject>().Initialize(_collectible, new Color(1 - r, 1 - g, 1 - b));

        if (_collectible == null)
            return;
    }

    void DisableSpawnColliders(bool isRecursive)
    {
        // normal 
        Vector3 pos = new Vector3(transform.position.x + 20f, transform.position.y, transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(pos, 2f);
        if (hitColliders.Length > 0)
        {
            positiveZCollider.SetActive(false);
            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        pos = new Vector3(transform.position.x - 20f, transform.position.y, transform.position.z);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            negativeZCollider.SetActive(false);
            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20f);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            positiveXCollider.SetActive(false);
            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20f);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            negativeXCollider.SetActive(false);
            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        // corners 
        pos = new Vector3(transform.position.x + 20f, transform.position.y, transform.position.z + 20f);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            cornerNegXPosZ.SetActive(false);

            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        pos = new Vector3(transform.position.x - 20f, transform.position.y, transform.position.z - 20f);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            cornerPosXNegZ.SetActive(false);

            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        pos = new Vector3(transform.position.x + 20f, transform.position.y, transform.position.z - 20f);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            cornerPosXPosZ.SetActive(false);

            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }

        pos = new Vector3(transform.position.x - 20f, transform.position.y, transform.position.z + 20f);
        hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            cornerNegXNegZ.SetActive(false);

            if (!isRecursive && hitColliders[0] != null)
                hitColliders[0].gameObject.GetComponent<Floor>().DisableSpawnColliders(true);
        }
    }
}
