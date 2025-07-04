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
    [Header("読み取り先スクリプト")]
    [SerializeField] private ColliderManager colliderManager;
    [SerializeField] private VectorManager vectorManager;
    [SerializeField] private JapaneseManager japaneseManager;
    [Header("フェードイン・アウト用画像")]
    [SerializeField] private GameObject FadeInOutImageObject;
    public GameObject fadeInOutImageObject => FadeInOutImageObject;
    [Header("セッティングメニュー")]
    [SerializeField] private GameObject SettingMenu;
    [Header("チェックイメージ")]
    [SerializeField] private GameObject CheckImage;
    [Header("成績表用スタンプ")]
    [SerializeField] public Image Stamp;
    [Header("ハンバーガーメニュー表示用ボタン")]
    [SerializeField] private Button HamburgerMenuButton;
    [Header("セッティングメニュー透明背後ボタン")]
    [SerializeField] private Button SettingMenuInvisibleImageButton;
    [Header("ポジションリセットボタン")]
    [SerializeField] private Button PositionResetButton;
    [Header("×ボタン")]
    [SerializeField] private Button XButton;
    [Header("学業成績表背景ボタン")]
    [SerializeField] private Button InvisibleTestPrintBackGroundImageButton;
    [Header("チェックボタン")]
    [SerializeField] private Button JapaneseCheckButton;
    [Header("スライダー類")]
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;
    [Header("音量用数値")]
    public float NowBGM;//今のBGMの音量を覚えておく変数
    public float NowSE;//今のSEの音量を覚えておく変数
    [Header("テスト用数値")]
    [SerializeField] private int JapaneseScore = -1;
    [SerializeField] private int SocialStudiesScore = -1;
    [SerializeField] private int MathematicsScore = -1;
    [SerializeField] private int ScienceScore = -1;
    [SerializeField] private int EnglishScore = -1;
    private int ChallengingSubjects = 0;
    private int OverallGradeScore = 0;
    [Header("テキスト類")]
    [Header("音量数値テキスト")]
    [SerializeField] private TextMeshProUGUI BGMText;
    [SerializeField] private TextMeshProUGUI SEText;
    [Header("成績表用テキスト")]
    [SerializeField] private TextMeshProUGUI JapaneseText;
    [SerializeField] private TextMeshProUGUI SocialStudiesText;
    [SerializeField] private TextMeshProUGUI MathematicsText;
    [SerializeField] private TextMeshProUGUI ScienceText;
    [SerializeField] private TextMeshProUGUI EnglishText;
    [SerializeField] private TextMeshProUGUI OverallGradeText;
    [Header("Japanese用テキスト")]
    [SerializeField] private TextMeshProUGUI CountDownText;
    // 読み取り専用プロパティ
    public TextMeshProUGUI countDownText => CountDownText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    // 読み取り専用プロパティ
    public TextMeshProUGUI scoreText => ScoreText;
    [Header("読み取り元データ")]
    [Header("今居る世界")]
    [SerializeField] private bool Dream = false;
    public bool dream => Dream;
    [Header("学業成績表イメージ")]
    [SerializeField] private GameObject TestPrint;
    public GameObject testPrint => TestPrint;
    [Header("Japaneseイメージ")]
    [SerializeField] private GameObject OperationManualImage;
    public GameObject operationManualImage => OperationManualImage;
    [Header("表示テキスト")]
    [SerializeField] private GameObject TextObject;
    // 読み取り専用プロパティ
    public GameObject textObject => TextObject;
    [SerializeField] private GameObject MindImage;
    // 読み取り専用プロパティ
    public GameObject mindImage => MindImage;
    [Header("学業成績表表示ボタン")]
    [SerializeField] private GameObject TestPrintButtonObject;
    public GameObject testPrintButtonObject => TestPrintButtonObject;
    [Header("カメラ")]
    [SerializeField] private Transform CameraTransform;
    // 読み取り専用プロパティ
    public Transform cameraTransform => CameraTransform;
    [Header("キャラクター")]
    [SerializeField] private Transform TargetPlayer;//追従するキャラクター
    public Transform targetPlayer => TargetPlayer;
    [Header("テキスト移動位置")]
    [SerializeField] private Transform DeskTextTransform;//机のテキスト位置
    //読み取り用
    public Transform deskTextTransform => DeskTextTransform;
    [SerializeField] private Transform BedTextTransform;//ベッドのテキスト位置
    //読み取り用
    public Transform bedTextTransform => BedTextTransform;
    [SerializeField] private Transform DoorTextTransform;//ドアのテキスト位置
    //読み取り用
    public Transform doorTextTransform => DoorTextTransform;
    [SerializeField] private Transform JapaneseTextTransform;//国語のテキスト位置
    //読み取り用
    public Transform japaneseTextTransform => JapaneseTextTransform;
    [SerializeField] private Transform SocialstudiesTextTransform;//社会のテキスト位置
    //読み取り用
    public Transform socialstudiesTextTransform => SocialstudiesTextTransform;
    [SerializeField] private Transform MathematicsTextTransform;//数学のテキスト位置
    //読み取り用
    public Transform mathematicsTextTransform => MathematicsTextTransform;
    [SerializeField] private Transform ScienseTextTransform;//理科のテキスト位置
    //読み取り用
    public Transform scienseTextTransform => ScienseTextTransform;
    [SerializeField] private Transform EnglishTextTransform;//英語のテキスト位置
    //読み取り用
    public Transform englishTextTransform => EnglishTextTransform;
    [SerializeField] private Transform ReturndoorTextTransform;//帰還用ドアのテキスト位置
    // //読み取り用
    public Transform returndoorTextTransform => ReturndoorTextTransform;
    [Header("キャラクターの移動・回転")]
    [SerializeField] private float MoveSpeed = 2f;
    [SerializeField] private float RotateSpeed = 3f;
    // 読み取り専用プロパティ
    public float moveSpeed => MoveSpeed;
    public float rotateSpeed => RotateSpeed;
    
    void Start()
    {
        //ボタンををした時の処理を追加
        HamburgerMenuButton.onClick.AddListener(hamburgermenubutton);
        SettingMenuInvisibleImageButton.onClick.AddListener(settingmenuinvisibleimage);
        XButton.onClick.AddListener(settingmenuinvisibleimage);
        PositionResetButton.onClick.AddListener(positionresetbutton);
        Button TestPrintButton = TestPrintButtonObject.GetComponentInChildren<Button>();
        TestPrintButton.onClick.AddListener(testprintbutton);
        InvisibleTestPrintBackGroundImageButton.onClick.AddListener(invisibletestprintbackgroundimagebutton);
        JapaneseCheckButton.onClick.AddListener(japanesecheckbutton);
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
        //ChallengingSubjects = 
    }
    /*
    void ()//
    {

    }
    */
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
        CameraTransform.transform.position = vectorManager.cameraFixedPosition;
        CameraTransform.transform.rotation = Quaternion.Euler(vectorManager.cameraFixedRotation);
        TargetPlayer.transform.position = vectorManager.characterFixedPosition;
    }
    void testprintbutton()//
    {
        TestPrintButtonObject.SetActive(false);
        TestPrint.SetActive(true);
    }
    void invisibletestprintbackgroundimagebutton()//
    {
        TestPrintButtonObject.SetActive(true);
        TestPrint.SetActive(false);
    }
    void japanesecheckbutton()//Japaneseの説明を次回起動時まで消す
    {
        CheckImage.SetActive(!CheckImage.activeSelf);
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