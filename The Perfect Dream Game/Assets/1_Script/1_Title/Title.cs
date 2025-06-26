using System.Collections;//コルーチンを使う用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Unityのシーン管理プログラムを扱えるようにする
using UnityEngine.UI;//UnityのUI関係を扱えるようにする
using TMPro;//UnityのTextMeshPro関係を扱えるようにする
using UnityEngine.Audio;//オーディオに関連するプログラムを扱えるようにする
using UnityEngine.Animations;//アニメーションに関連するプログラムを扱えるようにする

public class Title : MonoBehaviour
{
    //インスペクタ―上で、オブジェクトの指定などをしたい物を宣言していく
    //※SerializeFieldを書いていたらprivateでもインスペクタ―上で操作できるようになる
    /*
[SerializeField] private ;
    */
    [Header("フェードイン・アウト用画像")]
    [SerializeField] private GameObject FadeInOutImageObject;
    [Header("動画")]
    [SerializeField] private RawImage MoveRawImage;
    [Header("画面タップ用透明ボタン")]
    [SerializeField] private Button TapToStartButton;
    [Header("ハンバーガーメニュー表示用ボタン")]
    [SerializeField] private Button HamburgerMenuButton;
    [Header("セッティングメニュー")]
    [SerializeField] private GameObject SettingMenu;
    [Header("セッティングメニュー透明背後ボタン")]
    [SerializeField] private Button SettingMenuInvisibleImageButton;
    [Header("×ボタン")]
    [SerializeField] private Button XButton;
    [Header("スライダー類")]
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;
    [Header("テキスト類")]
    [SerializeField] private TextMeshProUGUI BGMText;
    [SerializeField] private TextMeshProUGUI SEText;
    [Header("点滅オブジェクト類")]
    [SerializeField] private GameObject LFadeInOutLoopImageObject;
    [SerializeField] private GameObject RFadeInOutLoopImageObject;
    [SerializeField] private GameObject FadeInOutTextObject;
    [Header("音量用数値")]
    public float NowBGM = 100f;//今のBGMの音量を覚えておく変数
    public float NowSE = 100f;//今のSEの音量を覚えておく変数
    //音量の最大、最小の値を設定する
    const float MIN = 0;     // 最小値
    const float MAX = 100;   // 最大値
    void Start()
    {
        //TapToStartButtonが押されたときに、(taptostartbutton)という名前のイベントリスナー(このイベントをするよ～的なやつ)を追加する
        TapToStartButton.onClick.AddListener(taptostartbutton);
        HamburgerMenuButton.onClick.AddListener(hamburgermenubutton);
        SettingMenuInvisibleImageButton.onClick.AddListener(settingmenuinvisibleimage);
        XButton.onClick.AddListener(settingmenuinvisibleimage);
        NowBGM = Mathf.Clamp(NowBGM, MIN, MAX);//BGMSliderの音量の範囲を０～１００の範囲に制限する
        NowSE = Mathf.Clamp(NowSE, MIN, MAX);//SESliderの音量の範囲を０～１００の範囲に制限する
        BGMSlider.value = NowBGM;//BGMSliderの音量をNowBGMの音量に変更する
        SESlider.value = NowSE;//SESliderの音量をNowSEの音量に変更する
        BGMText.text = NowBGM.ToString();//NowBGMの音量の数値をString型に変換し、BGMTextのText部分に入れる
        SEText.text = NowSE.ToString();//NowSEの音量の数値をSEString型に変換し、SETextのText部分に入れる
        FadeInOutImageObject.GetComponent<ImageFadeInOutScript>().StartCoroutine("IFadeIn");
        LFadeInOutLoopImageObject.GetComponent<LoopImageFadeInOutScript>().StartCoroutine("LIFade");
        RFadeInOutLoopImageObject.GetComponent<LoopImageFadeInOutScript>().StartCoroutine("LIFade");
        FadeInOutTextObject.GetComponent<LoopTextFadeInOutScript>().StartCoroutine("TFade");
        AudioManager.Instance.CrossFadeToBGM(0, 1.0f); // 1秒かけて最初のBGMを再生
    }
    void taptostartbutton()//taptostartbuttonという名前のイベントリスナーが実行されたときに実行する処理
    {
        LFadeInOutLoopImageObject.GetComponent<LoopImageFadeInOutScript>().StopCoroutine("LIFade");
        RFadeInOutLoopImageObject.GetComponent<LoopImageFadeInOutScript>().StopCoroutine("LIFade");
        FadeInOutTextObject.GetComponent<LoopTextFadeInOutScript>().StopCoroutine("TFade");
        StartCoroutine(TitleChangeScene());
        IEnumerator TitleChangeScene()
        {
            LFadeInOutLoopImageObject.GetComponent<FlashScript>().StartCoroutine("IFlashIn");
            RFadeInOutLoopImageObject.GetComponent<FlashScript>().StartCoroutine("IFlashIn");
            Coroutine wait1 = FadeInOutTextObject.GetComponent<FlashScript>().StartCoroutine("TFlashIn");
            yield return wait1;
            Coroutine wait2 = FadeInOutImageObject.GetComponent<ImageFadeInOutScript>().StartCoroutine("IFadeOut");
            yield return wait2;
            SceneManager.LoadScene("GameScene");//"GameScene"という名前のシーンを呼び出す
            //AudioManager.Instance.CrossFadeToSE(0, 0f); // 0秒かけて最初のBGMを再生
        }
    }

    void hamburgermenubutton()//SettingMenuを見えるようにする
    {
        SettingMenu.SetActive(true);
    }

    void settingmenuinvisibleimage()//SettingMenuを見えなくする
    {
        SettingMenu.SetActive(false);
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