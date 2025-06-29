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
    [SerializeField] private CameraMovingScript cameraMovingScript;
    private bool Dream;
    private RectTransform rectTransform;
    private Transform CameraTransform;
    private Transform TargetPlayer;
    private bool DeskHasSwitched = false;
    private bool BedHasSwitched = false;
    private bool DoorHasSwitched = false;
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
    private Image testPrint;
    void Start()
    {
        //private型のもの達に、vectorManagerのデータを入れておく

        // 読み取り専用プロパティ
        Dream = gamescenemanagerscript.dream;
        CameraTransform = gamescenemanagerscript.cameraTransform;
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
        Japanese = colliderManager.japanese;
        SocialStudies = colliderManager.socialStudies;
        Mathematics = colliderManager.mathematics;
        Science = colliderManager.science;
        English = colliderManager.english;
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
        if (other == UpDesk && !DeskHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))
        {
            DeskHasSwitched = true;
            CameraTransform.transform.position = vectorManager.deskCameraDestinationPosition;
            CameraTransform.transform.rotation = Quaternion.Euler(vectorManager.deskCameraDestinationRotation);
            TargetPlayer.transform.position = vectorManager.deskCharacterDestinationPosition;
        }
        // キーが離されたらリセットする
        if (Input.GetKeyUp(KeyCode.X) || Input.GetMouseButtonUp(0))
        {
            DeskHasSwitched = false;
        }
        if (other == Bed && !BedHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入っているコライダーがBedなら
        {
            BedHasSwitched = true;
            Dream = !Dream;
            Debug.Log(Dream);
        }
        // キーが離されたらリセットする
        if (Input.GetKeyUp(KeyCode.X) || Input.GetMouseButtonUp(0))
        {
            BedHasSwitched = false;
        }
        if (other == Door && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入っているコライダーがDoorなら
        {
            if (Dream == true)
            {
                DoorHasSwitched = true;
                CameraTransform.transform.position = vectorManager.dreamCameraDestinationPosition;
                CameraTransform.transform.rotation = Quaternion.Euler(vectorManager.dreamCameraDestinationRotation);
                TargetPlayer.transform.position = vectorManager.dreamCharacterDestinationPosition;
            }
            else
            {
                DoorHasSwitched = true;
                gamescenemanagerscript.mindImage.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.X) || Input.GetMouseButtonUp(0))
        {
            DoorHasSwitched = false;
        }
    }

    private void OnTriggerExit(Collider other)// 何かしらコライダーから出たときの処理
    {
        if (other == UpDesk)// 入ったコライダーがUpDeskなら
        {
            //カメラを定位置に戻す
            CameraTransform.transform.position = vectorManager.cameraFixedPosition;
            CameraTransform.transform.rotation = Quaternion.Euler(vectorManager.cameraFixedRotation);
        }
        if (other == Bed)// 入ったコライダーがBedなら
        {
            //カメラを定位置に戻す
            CameraTransform.transform.position = vectorManager.cameraFixedPosition;
            CameraTransform.transform.rotation = Quaternion.Euler(vectorManager.cameraFixedRotation);
        }
        if (other == Door)// 入ったコライダーがDoorなら
        {
            cameraMovingScript.enabled = true; // 強制的にONにする
        }
        OrTextObject.SetActive(false);//OrTextObjectを非表示にする
        gamescenemanagerscript.testPrint.SetActive(false);//testPrintを非表示にする
        MindImage.SetActive(false);//MindImageを非表示にする
    }
}