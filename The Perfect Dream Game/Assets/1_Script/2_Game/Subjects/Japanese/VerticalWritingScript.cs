using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerticalWritingScript : MonoBehaviour
{
    private TMP_Text textMeshPro;
    private void Awake()
    {
        textMeshPro = gameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textMeshPro.ForceMeshUpdate();

        var textInfo = textMeshPro.textInfo;
        if (textInfo.characterCount == 0)
        {
            return;
        }
        Vector3[] firstDestVertices = textInfo.meshInfo[0].vertices;
        float startX = firstDestVertices[0].x;
        Vector3 startCenter = (firstDestVertices[0] + firstDestVertices[2]) / 2;
        float characterHeight = firstDestVertices[1].y - firstDestVertices[0].y;

        for (int index = 0; index < textInfo.characterCount; index++)
        {
            var charaInfo = textInfo.characterInfo[index];
            if (!charaInfo.isVisible)
            {
                continue;
            }

            int materialIndex = charaInfo.materialReferenceIndex;
            int vertexIndex = charaInfo.vertexIndex;
            Vector3[] destVertices = textInfo.meshInfo[materialIndex].vertices;

            Vector3 charaCenter = (destVertices[vertexIndex + 0] + destVertices[vertexIndex + 2]) / 2;

            float offsetX = charaCenter.x - startCenter.x;
            float offsetY = (characterHeight * index);

            Vector3 offset = new Vector3(-offsetX, -offsetY, 0);

            destVertices[vertexIndex + 0] += offset;
            destVertices[vertexIndex + 1] += offset;
            destVertices[vertexIndex + 2] += offset;
            destVertices[vertexIndex + 3] += offset;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
