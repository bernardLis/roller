using UnityEngine;
using System.Collections.Generic;

public class MonumentMaker : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    GameObject platform;

    public void Init()
    {
        TidyUp();
        MakeFloor();
        MakeWalls();
        MakeRoof();
    }

    void TidyUp()
    {
        foreach (Transform c in transform)
            DestroyImmediate(c.gameObject);
    }

    void MakeFloor()
    {
        platform = new GameObject("Platform");
        platform.transform.parent = transform;

        float yMultiplier = 0.2f;
        int scaleStep = 2;
        int startValue = 20;

        for (int i = 0; i < 8; i++)
        {
            GameObject f = Instantiate(floorPrefab, new Vector3(0f, i * yMultiplier, 0f), Quaternion.identity);
            f.transform.localScale = new Vector3(startValue - (scaleStep * i), yMultiplier, startValue - (scaleStep * i));
            f.transform.parent = platform.transform;
        }
    }

    void MakeWalls()
    {
        List<GameObject> walls = new();
        for (int i = 0; i < 4; i++)
        {
            GameObject w = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
            walls.Add(w);
            w.transform.parent = transform;

            w.transform.localScale = new Vector3(0.2f, 10f, 10f);
        }
        walls[0].transform.position = new Vector3(-6.5f, 5f, -6.5f);
        walls[1].transform.position = new Vector3(6.5f, 5f, 6.5f);
        walls[2].transform.position = new Vector3(6.5f, 5f, -6.5f);
        walls[3].transform.position = new Vector3(-6.5f, 5f, 6.5f);

        walls[0].transform.localEulerAngles = new Vector3(0, 45f, 0f);
        walls[1].transform.localEulerAngles = new Vector3(0, 45f, 0f);
        walls[2].transform.localEulerAngles = new Vector3(0, -45f, 0f);
        walls[3].transform.localEulerAngles = new Vector3(0, -45f, 0f);
    }

    void MakeRoof()
    {
        GameObject roof = Instantiate(platform);
        roof.name = "Roof";
        roof.transform.position = new Vector3(0f, 10f, 0f);
        roof.transform.localEulerAngles = new Vector3(0f, 0f, -180f);

        roof.transform.parent = transform;
    }

}
