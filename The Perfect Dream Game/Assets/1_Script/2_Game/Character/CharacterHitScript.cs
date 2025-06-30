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
    private GameObject TextObject;
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
    private Transform DeskTextTransform;
    private Transform BedTextTransform;
    private Transform DoorTextTransform;
    private Transform TextTransform;
    private Transform JapaneseTextTransform;
    private Transform SocialstudiesTextTransform;
    private Transform MathematicsTextTransform;
    private Transform ScienseTextTransform;
    private Transform EnglishTextTransform;
    private Transform ReturndoorTextTransform;
    private Image testPrint;
    void Start()
    {
        //private型のもの達に、vectorManagerのデータを入れておく

        // 読み取り専用プロパティ
        Dream = gamescenemanagerscript.dream;
        CameraTransform = gamescenemanagerscript.cameraTransform;
        TargetPlayer = gamescenemanagerscript.targetPlayer;
        TextObject = gamescenemanagerscript.textObject;
        MindImage = gamescenemanagerscript.mindImage;
        DeskTextTransform = gamescenemanagerscript.deskTextTransform;
        BedTextTransform = gamescenemanagerscript.bedTextTransform;
        DoorTextTransform = gamescenemanagerscript.doorTextTransform;
        JapaneseTextTransform = gamescenemanagerscript.japaneseTextTransform;
        SocialstudiesTextTransform = gamescenemanagerscript.socialstudiesTextTransform;
        MathematicsTextTransform = gamescenemanagerscript.mathematicsTextTransform;
        ScienseTextTransform = gamescenemanagerscript.scienseTextTransform;
        EnglishTextTransform = gamescenemanagerscript.englishTextTransform;
        ReturndoorTextTransform = gamescenemanagerscript.returndoorTextTransform;
        DeskAnchoredPosition = vectorManager.deskAnchoredPosition;
        BedAnchoredPosition = vectorManager.bedAnchoredPosition;
        DoorAnchoredPosition = vectorManager.doorAnchoredPosition;
        DeskCameraDestinationPosition = vectorManager.deskCameraDestinationPosition;
        DeskCameraDestinationRotation = vectorManager.deskCameraDestinationRotation;
        UpDesk = colliderManager.upDesk;
        Bed = colliderManager.bed;
        Door = colliderManager.door;
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
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(DeskTextTransform);
            TextObject.transform.localPosition = Vector3.zero;// ローカル位置を (0, 0, 0) に設定
            TextObject.transform.rotation = Quaternion.Euler(vectorManager.cameraFixedRotation);
        }
        if (other == Bed)// 入ったコライダーがBedなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(BedTextTransform);
            TextObject.transform.localPosition = Vector3.zero;// ローカル位置を (0, 0, 0) に設定
            TextObject.transform.rotation = Quaternion.Euler(vectorManager.cameraFixedRotation);
        }
        if (other == Door)// 入ったコライダーがDoorなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(DoorTextTransform);
            TextObject.transform.localPosition = Vector3.zero;// ローカル位置を (0, 0, 0) に設定
            TextObject.transform.rotation = Quaternion.Euler(vectorManager.cameraFixedRotation);
        }
        if (other == Japanese)// 入ったコライダーがJapaneseなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(JapaneseTextTransform);
            TextObject.transform.localPosition = vectorManager.textColliderPosition;
            TextObject.transform.localRotation = Quaternion.Euler(vectorManager.textColliderRotation);
            TextObject.transform.localScale = Vector3.one;
        }
        if (other == SocialStudies)// 入ったコライダーがSocialStudiesなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(SocialstudiesTextTransform);
            TextObject.transform.localPosition = vectorManager.textColliderPosition;
            TextObject.transform.localRotation = Quaternion.Euler(vectorManager.textColliderRotation);
            TextObject.transform.localScale = Vector3.one;
        }
        if (other == Mathematics)// 入ったコライダーがMathematicsなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(MathematicsTextTransform);
            TextObject.transform.localPosition = vectorManager.textColliderPosition;
            TextObject.transform.localRotation = Quaternion.Euler(vectorManager.textColliderRotation);
            TextObject.transform.localScale = Vector3.one;
        }
        if (other == Science)// 入ったコライダーがScienceなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(ScienseTextTransform);
            TextObject.transform.localPosition = vectorManager.textColliderPosition;
            TextObject.transform.localRotation = Quaternion.Euler(vectorManager.textColliderRotation);
            TextObject.transform.localScale = Vector3.one;
        }
        if (other == English)// 入ったコライダーがEnglishなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(EnglishTextTransform);
            TextObject.transform.localPosition = vectorManager.textColliderPosition;
            TextObject.transform.localRotation = Quaternion.Euler(vectorManager.textColliderRotation);
            TextObject.transform.localScale = Vector3.one;
        }
        if (other == ReturnDoor)// 入ったコライダーがReturnDoorなら
        {
            TextObject.SetActive(true);//TextObjectを表示する
            TextObject.transform.SetParent(ReturndoorTextTransform);
            TextObject.transform.localPosition = vectorManager.textColliderPosition;
            TextObject.transform.localRotation = Quaternion.Euler(vectorManager.textColliderRotation);
            TextObject.transform.localScale = Vector3.one;
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
                cameraMovingScript.enabled = true; // 強制的にONにする
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
        if (other == Japanese && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入ったコライダーがJapaneseなら
        {
            cameraMovingScript.enabled = false; // 強制的にOFFにする
            CameraTransform.transform.position = vectorManager.japaneseCameraDestinationPosition;
            CameraTransform.transform.rotation = Quaternion.Euler(vectorManager.japaneseCameraDestinationRotation);
            TargetPlayer.transform.position = vectorManager.japaneseCharacterDestinationPosition;
        }
        if (other == SocialStudies && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入ったコライダーがSocialStudiesなら
        {

        }
        if (other == Mathematics && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入ったコライダーがMathematicsなら
        {

        }
        if (other == Science && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入ったコライダーがScienceなら
        {

        }
        if (other == English && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入ったコライダーがEnglishなら
        {

        }
        if (other == ReturnDoor && !DoorHasSwitched && (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)))// 入ったコライダーがReturnDoorなら
        {

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
        TextObject.SetActive(false);//TextObjectを非表示にする
        gamescenemanagerscript.testPrint.SetActive(false);//testPrintを非表示にする
        MindImage.SetActive(false);//MindImageを非表示にする
    }
}