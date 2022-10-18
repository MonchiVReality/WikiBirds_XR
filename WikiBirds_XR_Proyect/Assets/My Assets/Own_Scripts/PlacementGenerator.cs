using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlacementGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] prefab;
    [Header("Raycast Settings")]
    [SerializeField] int density;
    [Space]
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;

    [Header("Prefab Variation Settings")]
    [SerializeField, Range(0, 1)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;


    public void Awake()
    {
        Generate();
    }

    public void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    Generate();
        //}
    }

#if UNITY_EDITOR
    public void Generate()
    {

        for (int i = 0; i < density; ++i)
        {
            for (int w = 0; w < prefab.Length; ++w)
            {
                float sampleX = Random.Range(xRange.x, xRange.y);
                float sampleY = Random.Range(zRange.x, zRange.y);
                Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                    continue;

                int waterLayer = LayerMask.NameToLayer("Water");
                if (hit.transform.gameObject.layer == waterLayer)
                    continue;

                if (hit.point.y < minHeight)
                    continue;

                GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab[w], transform);
                instantiatedPrefab.transform.position = hit.point;
                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y) * 10, Space.Self);
                //instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
                instantiatedPrefab.transform.localScale = new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z));
            }
        }
    }

    public void clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
#endif
}
