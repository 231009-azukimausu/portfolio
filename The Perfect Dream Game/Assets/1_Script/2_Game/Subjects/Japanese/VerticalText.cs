using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerticalText : MonoBehaviour
{
    void Start()
    {
        var tmp = GetComponent<TextMeshPro>();
        if (tmp != null)
        {
            string original = tmp.text;
            string vertical = string.Join("\n", original.ToCharArray());
            tmp.text = vertical;
        }
    }
}