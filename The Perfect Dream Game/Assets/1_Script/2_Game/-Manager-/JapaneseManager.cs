using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UnityのUI関係を扱えるようにする
using TMPro;//UnityのTextMeshPro関係を扱えるようにする

public class JapaneseManager : MonoBehaviour
{

    [Header("読み取り先スクリプト")]
    [SerializeField] private GameSceneManagerScript gamescenemanagerscript;
    [SerializeField] private ColliderManager colliderManager;
    [SerializeField] private VectorManager vectorManager;
    [SerializeField] private CameraMovingScript cameraMovingScript;
    [Header("タイマーテキストオブジェクト")]
    [SerializeField] private GameObject TimerTextObject;
    [Header("カウントダウンテキストオブジェクト")]
    [SerializeField] private GameObject CountDownTextObject;
    [Header("スコアテキストオブジェクト")]
    [SerializeField] private GameObject ScoreTextObject;
    [Header("ゲームに進むボタン")]
    [SerializeField] private Button JapaneseNextButton;
    [Header("前に戻るボタン")]
    [SerializeField] private Button JapaneseBackButton;
    [Header("文字テキスト生成設定")]
    [SerializeField] private GameObject characterPrefab;//文字生成のプレハブ
    [SerializeField] private List<string> correctWords = new List<string> { "犬", "猫", "鳥" };//正解の文字
    public List<string> CorrectWords => correctWords;//読み取り専用
    [SerializeField] private List<string> wrongWords = new List<string> { "apple", "banana", "car" };//不正解の文字
    public List<string> WrongWords => wrongWords;//読み取り専用
    [SerializeField] private TMP_FontAsset correctFont;//正解のフォント
    public TMP_FontAsset CorrectFont => correctFont;//読み取り用
    [SerializeField] private TMP_FontAsset wrongFont;//不正解のフォント
    public TMP_FontAsset WrongFont => wrongFont;//読み取り用
    [SerializeField] private Transform spawnCenter;
    public Transform SpawnCenter => spawnCenter;//読み取り専用
    [SerializeField] private float spawnRange = 3f;
    public float SpawnRange => spawnRange;//読み取り専用
    [SerializeField] private float spawnInterval = 1.0f;
    public float SpawnInterval => spawnInterval;//読み取り専用
    [SerializeField] private float fallSpeed = 2f;
    public float FallSpeed => fallSpeed;//読み取り専用
    [SerializeField] private int correctCount = 10;
    public int CorrectCount => correctCount;//読み取り専用
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    public TMP_Text ScoreText => scoreText;//読み取り専用
    [SerializeField] private TMP_Text timerText;
    public TMP_Text TimerText => timerText;//読み取り専用
    [SerializeField] private TMP_Text countdownText;
    public TMP_Text CountdownText => countdownText;//読み取り専用
    [SerializeField] private GameObject startButton;
    public GameObject StartButton => startButton;//読み取り専用
    [SerializeField] private GameObject resultImage; // 非表示にしておく
    [SerializeField] private TMP_Text correctCountText;
    [SerializeField] private TMP_Text wrongCountText;
    [Header("制限時間")]
    [SerializeField] private float timeLimit = 30f;
    public float TimeLimit => timeLimit;
    // 内部変数（変更あり）
    private int correctCaught = 0;
    private int wrongCaught = 0;
    public void AddCorrectCount() => correctCaught++;
    public void AddWrongCount() => wrongCaught++;
    private int score = 0;
    private float timer = 0f;
    private bool isPlaying = false;
    private int correctSpawned = 0;
    private int startTime = 0;
    void Start()
    {
        countdownText.text = "";
        scoreText.text = "Score: 0";
        timerText.text = $"Time: {timeLimit:0}";
        JapaneseNextButton.onClick.AddListener(japanesenextbutton);
        JapaneseBackButton.onClick.AddListener(japanesebackbutton);
    }
    void Update()
    {
        if (!isPlaying) return;

        timer -= Time.deltaTime;
        timerText.text = $"Time: {timer:0}";

        if (timer <= 0f)
        {
            timer = 0f;
            isPlaying = false;
            EndGame();
        }
    }
    void japanesenextbutton()//次へを押した後の処理
    {
        TimerTextObject.SetActive(true);
        CountDownTextObject.SetActive(true);
        ScoreTextObject.SetActive(true);
        gamescenemanagerscript.operationManualImage.SetActive(false);
        StartCoroutine(CountdownAndStart());
    }
    void japanesebackbutton()//ドア先の位置に戻る
    {
        cameraMovingScript.enabled = true; // 強制的にOFFにする
        gamescenemanagerscript.fadeInOutImageObject.GetComponent<ImageFadeInOutScript>().StartCoroutine("IFadeIn");
        gamescenemanagerscript.cameraTransform.transform.position = vectorManager.dreamCameraDestinationPosition;
        gamescenemanagerscript.cameraTransform.transform.rotation = Quaternion.Euler(vectorManager.dreamCameraDestinationRotation);
        gamescenemanagerscript.targetPlayer.transform.position = vectorManager.dreamCharacterDestinationPosition;
        gamescenemanagerscript.operationManualImage.SetActive(false);
    }
    IEnumerator CountdownAndStart()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Start!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        isPlaying = true;
        timer = timeLimit;
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (isPlaying)
        {
            SpawnCharacter();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnCharacter()
    {
        bool isCorrect = (correctSpawned < correctCount && Random.value < 0.5f);
        string selectedWord;
        TMP_FontAsset fontToUse;

        if (isCorrect)
        {
            selectedWord = correctWords[Random.Range(0, correctWords.Count)];
            fontToUse = correctFont;
            correctSpawned++;
        }
        else
        {
            selectedWord = wrongWords[Random.Range(0, wrongWords.Count)];
            fontToUse = wrongFont;
        }

        Vector3 pos = spawnCenter.position + new Vector3(
            Random.Range(-spawnRange, spawnRange), 0, 0);

        GameObject obj = Instantiate(characterPrefab, pos, Quaternion.identity);

        FallingChar fallingChar = obj.GetComponent<FallingChar>();
        fallingChar.Setup(selectedWord, isCorrect, fallSpeed, fontToUse);
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
    private void EndGame()
    {
        countdownText.text = "終了";
        resultImage.SetActive(true);

        correctCountText.text = $"正解数: {correctCaught}";
        wrongCountText.text = $"不正解数: {wrongCaught}";
    }
    public void RestartGame()
    {
        // スコア・カウントリセット
        score = 0;
        correctCaught = 0;
        wrongCaught = 0;
        timer = startTime;
        correctSpawned = 0;

        // UI 更新
        scoreText.text = $"Score: {score}";
        timerText.text = $"Time: {timer/*:0*/}";
        countdownText.text = "";

        // リザルト画面非表示
        resultImage.SetActive(false);

        // 画面上に残ってる文字をすべて削除
        foreach (var obj in GameObject.FindGameObjectsWithTag("FallingChar"))
        {
            Destroy(obj);
        }

        // 再スタート処理（コルーチン）
        StartCoroutine(CountdownAndStart());
    }
}
