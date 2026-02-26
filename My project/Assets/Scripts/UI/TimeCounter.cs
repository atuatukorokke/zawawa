using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter Instance;

    private float elapsedTime = 0f;
    private bool isCounting = false;

    [SerializeField] private Text timeText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "PlayerScene 1")
        {
            StartTimer();
        }
    }
    void Update()
    {
        if (!isCounting) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        if (timeText != null)
        {
            timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;   // 念のためリセット
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    // シーン切替時にUIを取り直す
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject obj = GameObject.Find("TimeText");
        if (obj != null)
        {
            timeText = obj.GetComponent<Text>();
        }
    }
}