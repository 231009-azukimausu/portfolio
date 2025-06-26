using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSpawner : MonoBehaviour
{
    public GameObject correctTextPrefab;
    public GameObject wrongTextPrefab;
    public float spawnInterval = 1.0f;
    public float spawnRangeX = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            timer = 0;
            SpawnText();
        }
    }

    void SpawnText()
    {
        bool isCorrect = Random.value > 0.5f;
        GameObject prefab = isCorrect ? correctTextPrefab : wrongTextPrefab;

        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 10f, 0f);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}