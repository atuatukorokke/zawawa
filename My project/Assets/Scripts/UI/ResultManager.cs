using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text resultText;

    void Start()
    {
        float time = TimeCounter.Instance.GetTime();

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        resultText.text = "CLEAR TIME\n" +
                          minutes.ToString("00") + ":" +
                          seconds.ToString("00");
    }
}