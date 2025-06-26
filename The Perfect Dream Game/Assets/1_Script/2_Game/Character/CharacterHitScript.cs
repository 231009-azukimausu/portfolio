using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;//UnityのTextMeshPro関係を扱えるようにする

public class CharacterHitScript : MonoBehaviour
{
    //読み取り先スクリプト
    [SerializeField] private GameSceneManagerScript gamescenemanagerscript;
    [SerializeField] private ColliderManager colliderManager;
    [SerializeField] private VectorManager vectorManager;
    [HideInInspector] public bool Dream = false;
    private RectTransform rectTransform;
    private Transform TargetPlayer;
    //ターゲットコライダー
    private Collider UpDesk;
    private Collider Bed;
    private Collider Door;
    //表示テキスト
    private GameObject OrTextObject;
    private GameObject MindImage;
    //テキスト移動先位置
    private Vector2 DeskAnchoredPosition;
    private Vector2 BedAnchoredPosition;
    private Vector2 DoorAnchoredPosition;
    private Vector2 DeskCameraDestinationPosition;
    private Vector2 DeskCameraDestinationRotation;
    private Collider ReturnDoor;
    private Collider English;
    private Collider Science;
    private Collider Mathematics;
    private Collider Japanese;
    private Collider SocialStudies;
    private Collider Ahead;
    void Start()
    {
        //private型のもの達に、vectorManagerのデータを入れておく
        TargetPlayer = gamescenemanagerscript.targetPlayer;
        UpDesk = colliderManager.upDesk;
        Bed = colliderManager.bed;
        Door = colliderManager.door;
        OrTextObject = gamescenemanagerscript.orTextObject;
        MindImage = gamescenemanagerscript.mindImage;
        DeskAnchoredPosition = vectorManager.deskAnchoredPosition;
        BedAnchoredPosition = vectorManager.bedAnchoredPosition;
        DoorAnchoredPosition = vectorManager.doorAnchoredPosition;
        DeskCameraDestinationPosition = vectorManager.deskCameraDestinationPosition;
        DeskCameraDestinationRotation = vectorManager.deskCameraDestinationRotation;
        ReturnDoor = colliderManager.returnDoor;
        English = colliderManager.english;
        Science = colliderManager.science;
        Mathematics = colliderManager.mathematics;
        Japanese = colliderManager.japanese;
        SocialStudies = colliderManager.socialStudies;
        Ahead = colliderManager.ahead;
    }

    private void OnTriggerEnter(Collider other)// 入ったときの処理
    {
        if (other == UpDesk)// 入ったコライダーがUpDeskなら
        {
            OrTextObject.SetActive(true);// OrTextObjectを表示する
            rectTransform = OrTextObject.GetComponent<RectTransform>();// RectTransform を取得
            rectTransform.anchoredPosition = DeskAnchoredPosition;// 位置を変更
        }
        if (other == Bed)// 入ったコライダーがBedなら
        {
            OrTextObject.SetActive(true);//OrTextObjectを表示する
            rectTransform = OrTextObject.GetComponent<RectTransform>();// RectTransform を取得
            rectTransform.anchoredPosition = BedAnchoredPosition;// 位置を変更
        }
        if (other == Door)// 入ったコライダーがDoorなら
        {
            OrTextObject.SetActive(true);//OrTextObjectを表示する
            rectTransform = OrTextObject.GetComponent<RectTransform>();// RectTransform を取得
            rectTransform.anchoredPosition = DoorAnchoredPosition;// 位置を変更
        }
    }
    void OnTriggerStay(Collider other)
    {
        // 入っているコライダーがUpDeskかつ、Xボタンもしくは左クリックを押したら
        if (other == UpDesk && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))
        {
            //DeskCameraDestinationPosition
            //DeskCameraDestinationRotation
        }
        if (other == Bed && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入っているコライダーがBedなら
        {
            Dream = !Dream;
        }
        if (other == Door && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入っているコライダーがDoorなら
        {
            if (Dream == true)
            {
                //WarpToPosition(targetPosition);
            }
            else
            {
                MindImage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)// 何かしらコライダーから出たときの処理
    {
        OrTextObject.SetActive(false);//OrTextObjectを非表示にする
        MindImage.SetActive(false);//MindImageを非表示にする
    }
}