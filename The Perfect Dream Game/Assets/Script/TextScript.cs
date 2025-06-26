using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class TextScript : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TextMeshProUGUI Messages;
    [SerializeField] private string[] lines;
    [SerializeField] private float TextWaitSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        DialoguePanel.SetActive(true);
        Messages.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Messages.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                Messages.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1 (各文字を１つずつ入力してください)
        foreach (char c in lines[index].ToCharArray())
        {
            Messages.text += c;
            yield return new WaitForSeconds(TextWaitSpeed); //文字送りTextWaitSpeed秒待ってね～
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            Messages.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialoguePanel.SetActive(false);
        }
    }
}
