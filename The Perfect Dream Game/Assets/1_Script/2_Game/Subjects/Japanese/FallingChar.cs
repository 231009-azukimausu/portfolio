using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//UnityのTextMeshPro関係を扱えるようにする

[RequireComponent(typeof(BoxCollider))]
public class FallingChar : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Rigidbody rb;
    private BoxCollider box;

    private bool isCorrect;

    public void Setup(string text, bool correct, float fallSpeed, TMP_FontAsset font)
    {
        isCorrect = correct;

        textMesh = GetComponentInChildren<TextMeshPro>();
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();

        // フォント設定
        textMesh.font = font;

        // 文字の設定
        textMesh.text = correct ? ToVertical(text) : text;

        // 落下速度
        rb.velocity = Vector3.down * fallSpeed;

        // コライダー調整
        AdjustColliderSize();
    }

    private string ToVertical(string input)
    {
        return string.Join("\n", input.ToCharArray());
    }

    private void AdjustColliderSize()
    {
        if (textMesh == null || box == null) return;

        // TextMeshProのメッシュ情報を更新
        textMesh.ForceMeshUpdate();

        // ワールド空間でのバウンディングボックス
        Bounds bounds = textMesh.renderer.bounds;

        // ワールド空間のサイズをローカル空間に変換
        Vector3 worldSize = bounds.size;
        Vector3 localSize = transform.InverseTransformVector(worldSize);

        localSize.z = 5.5f;

        // コライダーのサイズを調整、中心は常に (0,0,0)
        box.size = localSize;
        box.center = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JapaneseManager jm = FindObjectOfType<JapaneseManager>();

            if (isCorrect)
            {
                jm.AddScore(+10);
                jm.AddCorrectCount(); // ← 追加
            }
            else
            {
                jm.AddScore(-5);
                jm.AddWrongCount(); // ← 追加
            }

            Destroy(gameObject);
        }
    }
}