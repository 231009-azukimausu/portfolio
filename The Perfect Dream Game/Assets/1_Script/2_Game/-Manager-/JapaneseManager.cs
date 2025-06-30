using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UnityのUI関係を扱えるようにする
using TMPro;//UnityのTextMeshPro関係を扱えるようにする

public class JapaneseManager : MonoBehaviour
{
    //読み取り先スクリプト
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
    [Header("生成設定")]
    [SerializeField] private GameObject[] correctPrefabs;
    public GameObject[] CorrectPrefabs => correctPrefabs;

    [SerializeField] private GameObject[] wrongPrefabs;
    public GameObject[] WrongPrefabs => wrongPrefabs;

    [SerializeField] private Transform spawnCenter;
    public Transform SpawnCenter => spawnCenter;

    [SerializeField] private float spawnRange = 3f;
    public float SpawnRange => spawnRange;

    [SerializeField] private float spawnInterval = 1.0f;
    public float SpawnInterval => spawnInterval;

    [SerializeField] private float fallSpeed = 2f;
    public float FallSpeed => fallSpeed;

    [SerializeField] private int correctCount = 10;
    public int CorrectCount => correctCount;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    public TMP_Text ScoreText => scoreText;

    [SerializeField] private TMP_Text timerText;
    public TMP_Text TimerText => timerText;

    [SerializeField] private TMP_Text countdownText;
    public TMP_Text CountdownText => countdownText;

    [SerializeField] private GameObject startButton;
    public GameObject StartButton => startButton;

    [Header("制限時間")]
    [SerializeField] private float timeLimit = 30f;
    public float TimeLimit => timeLimit;

    // 内部変数（変更あり）
    private int score = 0;
    private float timer = 0f;
    private bool isPlaying = false;
    private int correctSpawned = 0;

    void Start()
    {
        countdownText.text = "";
        scoreText.text = "Score: 0";
        timerText.text = $"Time: {timeLimit:0}";
        JapaneseNextButton.onClick.AddListener(japanesenextbutton);
        JapaneseBackButton.onClick.AddListener(japanesebackbutton);
    }
    void japanesenextbutton()//次へを押した後の処理
    {
        TimerTextObject.SetActive(true);
        CountDownTextObject.SetActive(true);
        ScoreTextObject.SetActive(true);
        gamescenemanagerscript.japaneseImageObject.SetActive(false);
        StartCoroutine(CountdownAndStart());
    }
    void japanesebackbutton()//ドア先の位置に戻る
    {
        cameraMovingScript.enabled = true; // 強制的にOFFにする
        gamescenemanagerscript.fadeInOutImageObject.GetComponent<ImageFadeInOutScript>().StartCoroutine("IFadeIn");
        gamescenemanagerscript.cameraTransform.transform.position = vectorManager.dreamCameraDestinationPosition;
        gamescenemanagerscript.cameraTransform.transform.rotation = Quaternion.Euler(vectorManager.dreamCameraDestinationRotation);
        gamescenemanagerscript.targetPlayer.transform.position = vectorManager.dreamCharacterDestinationPosition;
        gamescenemanagerscript.japaneseImageObject.SetActive(false);
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

    void Update()
    {
        if (!isPlaying) return;

        timer -= Time.deltaTime;
        timerText.text = $"Time: {timer:0}";

        if (timer <= 0f)
        {
            isPlaying = false;
            // 終了処理追加予定
        }
    }

    void SpawnCharacter()
    {
        GameObject prefab;

        if (correctSpawned < correctCount && Random.value < 0.5f)
        {
            prefab = correctPrefabs[Random.Range(0, correctPrefabs.Length)];
            correctSpawned++;
        }
        else
        {
            prefab = wrongPrefabs[Random.Range(0, wrongPrefabs.Length)];
        }

        Vector3 pos = spawnCenter.position + new Vector3(
            Random.Range(-spawnRange, spawnRange), // X方向のみランダム
            0,// Y方向は固定
            0); // Z方向は固定

        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = Vector3.down * fallSpeed;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
