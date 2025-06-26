using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScriptPro : MonoBehaviour
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private GameObject NameBoxObject;
    [SerializeField] private GameObject TextBoxObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;
 
    private Coroutine dialogueCoroutine;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 会話文表示処理を実行する
            displayDialogueText();
        }
    }
 
    /// <summary>
    /// 会話文表示処理
    /// </summary>
    public void displayDialogueText()
    {
        // パネルが非表示なら表示する
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);
            NameBoxObject.SetActive(true);
            TextBoxObject.SetActive(true);
        }
 
        // scriptableObjectの情報をパネルに表示する
        speakerNameText.text = dialogueText.speakerName;
        if (dialogueText.paragraphs.Length > index)
        {
            if (!isTyping)
            {
                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.paragraphs[index]));
            } else
            {
                stopTyping();
            }
        } else
        {
            // 会話が終了したためパネルを非表示にする
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);
            NameBoxObject.SetActive(false);
            TextBoxObject.SetActive(false);
        }
    }
 
    /// <summary>
    /// ダイアログのテキストを一文字づつ表示していきます
    /// </summary>
    /// <param name="paragraph">会話の1文</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator TypeDialogueText(string paragraph)
    {
        string displayText = "";
        isTyping = true;
        int colorIndex = 0;
        foreach(char c in paragraph)
        {
            colorIndex++;
            speakerDialogueText.text = paragraph;
            displayText = speakerDialogueText.text.Insert(colorIndex, "<color=#00000000>");
            speakerDialogueText.text = displayText;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        index++; //次の会話文インデックス
    }
 
    /// <summary>
    /// 1文字づつ表示を終了しテキスト全文を表示する
    /// </summary>
    private void stopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        // 未表示の会話文を全文表示する
        speakerDialogueText.text = dialogueText.paragraphs[index];
        isTyping = false;
        index++; //次の会話文インデックス
    }
}