using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//UnityのTextMeshPro関係を扱えるようにする

[RequireComponent(typeof(BoxCollider))]
public class FallingCher : MonoBehaviour
{
    public bool isCorrect;

    private void Start()
    {
        AdjustColliderSize();
    }

    private void AdjustColliderSize()
    {
        TextMeshPro tmp = GetComponentInChildren<TextMeshPro>();
        BoxCollider box = GetComponent<BoxCollider>();

        if (tmp != null && box != null)
        {
            // 表示が更新されるのを待つ
            tmp.ForceMeshUpdate();

            // バウンディングボックスからサイズ取得
            Bounds bounds = tmp.renderer.bounds;

            // ワールド空間のサイズをローカルに変換
            Vector3 localSize = transform.InverseTransformVector(bounds.size);

            box.size = localSize;
            box.center = transform.InverseTransformPoint(bounds.center) - transform.localPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int point = isCorrect ? +10 : -5;
            FindObjectOfType<JapaneseManager>().AddScore(point);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
