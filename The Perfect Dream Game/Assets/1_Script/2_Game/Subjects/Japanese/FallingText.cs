using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingText : MonoBehaviour
{
    public float fallSpeed = 3f;
    public bool isCorrect;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isCorrect)
            {
                ScoreManager.Instance.AddScore(1);
            }
            else
            {
                ScoreManager.Instance.AddScore(-1);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}