using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/new Dialogue Container")]
/*  CreateAssetMenu
    menuNameを決め、その名前でアセット化
*/
public class DialogueText : ScriptableObject
/*  ScriptableObject
    ゲームやアプリの中で変化せず、あちこちで共用するデータを格納する時に便利なクラス、
    個別のゲームオブジェクト等にアタッチはせず、都度アセットをロードして使用することになる
    ゲームが終了した後にデータは全て初期値に戻る
*/
{
    public string speakerName;
 
    [TextArea(5,10)]
    public string[] paragraphs;
}