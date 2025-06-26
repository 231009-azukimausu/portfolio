using System.Collections;//コルーチンを使う用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Unityのシーン管理プログラムを扱えるようにする
using UnityEngine.UI;//UnityのUI関係を扱えるようにする
using TMPro;//UnityのTextMeshPro関係を扱えるようにする
using UnityEngine.Audio;//オーディオに関連するプログラムを扱えるようにする
using UnityEngine.Animations;//アニメーションに関連するプログラムを扱えるようにする

public class GameSceneManagerScript : MonoBehaviour
{
    //インスペクタ―上で、オブジェクトの指定などをしたい物を宣言していく
    //※HideInInspectorを書いていたらpublicでもインスペクタ―上で操作できなくする
    //※SerializeFieldを書いていたらprivateでもインスペクタ―上で操作できるようになる
    /*
[HideInInspector] public ;
[SerializeField] private ;
    */
    //読み取り先スクリプト
    [SerializeField] private ColliderManager colliderManager;
    [SerializeField] private VectorManager vectorManager;
    [SerializeField] private JapaneseManager japaneseManager;
    [Header("フェードイン・アウト用画像")]
    [SerializeField] private GameObject FadeInOutImageObject;
    [Header("ハンバーガーメニュー表示用ボタン")]
    [SerializeField] private Button HamburgerMenuButton;
    [Header("セッティングメニュー")]
    [SerializeField] private GameObject SettingMenu;
    [Header("セッティングメニュー透明背後ボタン")]
    [SerializeField] private Button SettingMenuInvisibleImageButton;
    [Header("ポジションリセットボタン")]
    [SerializeField] private Button PositionResetButton;
    [Header("×ボタン")]
    [SerializeField] private Button XButton;
    [Header("スライダー類")]
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;
    [Header("テキスト類")]
    [SerializeField] private TextMeshProUGUI BGMText;
    [SerializeField] private TextMeshProUGUI SEText;
    [Header("音量用数値")]
    public float NowBGM;//今のBGMの音量を覚えておく変数
    public float NowSE;//今のSEの音量を覚えておく変数
    [Header("テスト用スクリプト")]
    //読み取り先スクリプト
    //[SerializeField] private;
    //[SerializeField] private Japanese japanese;
    [Header("テスト用数値")]
    [SerializeField] private int JapaneseScore = -1;
    //[SerializeField] private int SocialStudiesScore = -1;
    //[SerializeField] private int MathematicsScore = -1;
    //[SerializeField] private int ScienceScore = -1;
    //[SerializeField] private int EnglishScore = -1;
    private int ChallengingSubjects = 0;
    private int OverallGradeScore = 0;
    [Header("成績表用テキスト")]
    [SerializeField] private TextMeshProUGUI JapaneseText;
    /*
    [SerializeField] private TextMeshProUGUI SocialStudiesText;
    [SerializeField] private TextMeshProUGUI MathematicsText;
    [SerializeField] private TextMeshProUGUI ScienceText;
    [SerializeField] private TextMeshProUGUI EnglishText;
    [SerializeField] private TextMeshProUGUI OverallGradeText;
    */
    [Header("読み取り元データ")]
    [Header("今居る世界")]
    public bool Dream = false;
    //dream = !dream; //反転させる
    [Header("カメラ")]
    [SerializeField] private Transform CameraTransform;
    // 読み取り専用プロパティ
    public Transform cameraTransform => CameraTransform;
    [Header("キャラクター")]
    [SerializeField] private Transform TargetPlayer;//追従するキャラクター
    public Transform targetPlayer => TargetPlayer;
    [Header("キャラクターの移動・回転")]
    [SerializeField] private float MoveSpeed = 2f;
    [SerializeField] private float RotateSpeed = 3f;
    // 読み取り専用プロパティ
    public float moveSpeed => MoveSpeed;
    public float rotateSpeed => RotateSpeed;
    [Header("部屋でのコライダー")]
    [SerializeField] private Collider UpDesk;
    [SerializeField] private Collider Bed;
    [SerializeField] private Collider Door;
    // 読み取り専用プロパティ
    public Collider upDesk => UpDesk;
    public Collider bed => Bed;
    public Collider door => Door;
    [Header("夢でのコライダー")]
    [SerializeField] private Collider ReturnDoor;
    [SerializeField] private Collider English;
    [SerializeField] private Collider Science;
    [SerializeField] private Collider Mathematics;
    [SerializeField] private Collider Japanese;
    [SerializeField] private Collider SocialStudies;
    [SerializeField] private Collider Ahead;
    // 読み取り専用プロパティ
    public Collider returnDoor => ReturnDoor;
    public Collider english => English;
    public Collider science => Science;
    public Collider mathematics => Mathematics;
    public Collider japanese => Japanese;
    public Collider socialStudies => SocialStudies;
    public Collider ahead => Ahead;
    [Header("表示テキスト")]
    [SerializeField] private GameObject OrTextObject;
    // 読み取り専用プロパティ
    public GameObject orTextObject => OrTextObject;
    [SerializeField] private GameObject MindImage;
    // 読み取り専用プロパティ
    public GameObject mindImage => MindImage;
    // 読み取り専用プロパティ
    // カメラの定位置
    private Vector3 CameraFixedPosition;
    private Vector3 CameraFixedRotation;
    // キャラクターの定位置
    private Vector3 CharacterFixedPosition;
    // カメラとキャラの移動先位置
    // 机のカメラ位置
    private Vector3 DeskCameraDestinationPosition;
    private Vector3 DeskCameraDestinationRotation;
    // 机のキャラクター位置
    private Vector3 DeskCharacterDestinationPosition;
    // ドアの先のカメラ位置
    private Vector3 DreamCameraDestinationPosition;
    private Vector3 DreamCameraDestinationRotation;
    // ドアの先のキャラクター位置
    private Vector3 DreamCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 dreamCharacterDestinationPosition => DreamCharacterDestinationPosition;
    // 国語のドアの先のカメラ位置
    private Vector3 JapaneseCameraDestinationPosition;
    private Vector3 JapaneseCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 japaneseCameraDestinationPosition => JapaneseCameraDestinationPosition;
    public Vector3 japaneseCameraDestinationRotation => JapaneseCameraDestinationRotation;
    // 国語のドアの先のキャラクター位置
    private Vector3 JapaneseCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 japaneseCharacterDestinationPosition => JapaneseCharacterDestinationPosition;
    // 社会のドアの先のカメラ位置
    private Vector3 SocialStudiesCameraDestinationPosition;
    private Vector3 SocialStudiesCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 socialStudiesCameraDestinationPosition => SocialStudiesCameraDestinationPosition;
    public Vector3 socialStudiesCameraDestinationRotation => SocialStudiesCameraDestinationRotation;
    // 社会のドアの先のキャラクター位置
    private Vector3 SocialStudiesCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 socialStudiesCharacterDestinationPosition => SocialStudiesCharacterDestinationPosition;
    // 数学のドアの先のカメラ位置
    private Vector3 MathematicsCameraDestinationPosition;
    private Vector3 MathematicsCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 mathematicsCameraDestinationPosition => MathematicsCameraDestinationPosition;
    public Vector3 mathematicsCameraDestinationRotation => MathematicsCameraDestinationRotation;
    // 数学のドアの先のキャラクター位置
    private Vector3 MathematicsCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 mathematicsCharacterDestinationPosition => MathematicsCharacterDestinationPosition;
    // 理科のドアの先のカメラ位置
    private Vector3 ScienceCameraDestinationPosition;
    private Vector3 ScienceCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 scienceCameraDestinationPosition => ScienceCameraDestinationPosition;
    public Vector3 scienceCameraDestinationRotation => ScienceCameraDestinationRotation;
    // 理科のドアの先のキャラクター位置
    private Vector3 ScienceCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 scienceCharacterDestinationPosition => ScienceCharacterDestinationPosition;
    // 英語のドアの先のカメラ位置
    private Vector3 EnglishCameraDestinationPosition;
    private Vector3 EnglishCameraDestinationRotation;
    // 読み取り専用プロパティ
    public Vector3 englishCameraDestinationPosition => EnglishCameraDestinationPosition;
    public Vector3 englishCameraDestinationRotation => EnglishCameraDestinationRotation;
    // 英語のドアの先のキャラクター位置
    private Vector3 EnglishCharacterDestinationPosition;
    // 読み取り専用プロパティ
    public Vector3 englishCharacterDestinationPosition => EnglishCharacterDestinationPosition;
    // テキスト移動先位置
    private Vector2 DeskAnchoredPosition;
    private Vector2 BedAnchoredPosition;
    private Vector2 DoorAnchoredPosition;
    void Start()
    {
        HamburgerMenuButton.onClick.AddListener(hamburgermenubutton);
        SettingMenuInvisibleImageButton.onClick.AddListener(settingmenuinvisibleimage);
        XButton.onClick.AddListener(settingmenuinvisibleimage);
        PositionResetButton.onClick.AddListener(positionresetbutton);
        BGMSlider.value = NowBGM;//BGMSliderの音量をNowBGMの音量に変更する
        SESlider.value = NowSE;//SESliderの音量をNowSEの音量に変更する
        BGMText.text = NowBGM.ToString();//NowBGMの音量の数値をString型に変換し、BGMTextのText部分に入れる
        SEText.text = NowSE.ToString();//NowSEの音量の数値をSEString型に変換し、SETextのText部分に入れる
        FadeInOutImageObject.GetComponent<ImageFadeInOutScript>().StartCoroutine("IFadeIn");
        //SE再生（0番の曲）
        //AudioManager.Instance.PlaySE(1);
        /*
        //BGM再生（0番の曲）
        AudioManager.Instance.PlayBGM(1);
        //SE再生（1番の効果音）
        AudioManager.Instance.PlaySE(1);
        //クロスフェードでBGM切り替え（2番の曲、1.5秒かけて）
        AudioManager.Instance.CrossFadeToBGM(2, 1.5f);
        */
        CameraFixedPosition = vectorManager.cameraFixedPosition;
        CameraFixedRotation = vectorManager.cameraFixedRotation;
        CharacterFixedPosition = vectorManager.characterFixedPosition;
        DeskCameraDestinationPosition = vectorManager.deskCameraDestinationPosition;
        DeskCameraDestinationRotation = vectorManager.deskCameraDestinationRotation;
        DeskCharacterDestinationPosition = vectorManager.deskCharacterDestinationPosition;
        DreamCameraDestinationPosition = vectorManager.dreamCameraDestinationPosition;
        DreamCameraDestinationRotation = vectorManager.dreamCameraDestinationRotation;
        DreamCharacterDestinationPosition = vectorManager.dreamCharacterDestinationPosition;
        JapaneseCameraDestinationPosition = vectorManager.japaneseCameraDestinationPosition;
        JapaneseCameraDestinationRotation = vectorManager.japaneseCameraDestinationRotation;
        JapaneseCharacterDestinationPosition = vectorManager.japaneseCharacterDestinationPosition;
        SocialStudiesCameraDestinationPosition = vectorManager.socialStudiesCameraDestinationPosition;
        SocialStudiesCameraDestinationRotation = vectorManager.socialStudiesCameraDestinationRotation;
        SocialStudiesCharacterDestinationPosition = vectorManager.socialStudiesCharacterDestinationPosition;
        MathematicsCameraDestinationPosition = vectorManager.mathematicsCameraDestinationPosition;
        MathematicsCameraDestinationRotation = vectorManager.mathematicsCameraDestinationRotation;
        MathematicsCharacterDestinationPosition = vectorManager.mathematicsCharacterDestinationPosition;
        ScienceCameraDestinationPosition = vectorManager.scienceCameraDestinationPosition;
        ScienceCameraDestinationRotation = vectorManager.scienceCameraDestinationRotation;
        ScienceCharacterDestinationPosition = vectorManager.scienceCharacterDestinationPosition;
        EnglishCameraDestinationPosition = vectorManager.englishCameraDestinationPosition;
        EnglishCameraDestinationRotation = vectorManager.englishCameraDestinationRotation;
        EnglishCharacterDestinationPosition = vectorManager.englishCharacterDestinationPosition;
        DeskAnchoredPosition = vectorManager.deskAnchoredPosition;
        BedAnchoredPosition = vectorManager.bedAnchoredPosition;
        DoorAnchoredPosition = vectorManager.doorAnchoredPosition;
    }
    void hamburgermenubutton()//SettingMenuを見えるようにする
    {
        SettingMenu.SetActive(true);
    }

    void settingmenuinvisibleimage()//SettingMenuを見えなくする
    {
        SettingMenu.SetActive(false);
    }
    void positionresetbutton()//キャラクターをワープさせる
    {
        CameraTransform.transform.position = CameraFixedPosition;
        CameraTransform.transform.position = CameraFixedRotation;
        TargetPlayer.transform.position = CharacterFixedPosition;
    }
    public void BGMSliderMethod()//BGMSlider型に変更があった時に実行する変数
    {
        NowBGM = BGMSlider.value;//NowBGMの音量をBGMSliderの音量に変更する
        BGMText.text = NowBGM.ToString();//NowBGMの音量の数値をString型に変換し、BGMTextのText部分に入れる
    }

    public void SESliderMethod()//SESlider型に変更があった時に実行する変数
    {
        NowSE = SESlider.value;//NowSEの音量の数値をSESliderの音量に変更する
        SEText.text = NowSE.ToString();//NowSEの音量の数値をSEString型に変換し、SETextのText部分に入れる
    }
}