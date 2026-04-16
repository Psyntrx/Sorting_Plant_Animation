using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerScript : MonoBehaviour
{
    public GameObject[] prefabs;   // Add cube, cylinder, etc
    public float spawnInterval = 1f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        GameObject selectedPrefab = prefabs[randomIndex];

        Instantiate(selectedPrefab, transform.position, selectedPrefab.transform.rotation);
    }
}