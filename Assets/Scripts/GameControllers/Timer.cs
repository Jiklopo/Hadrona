using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public float time { get; private set; } = 0;
    
    [SerializeField] private bool stopped;
    public static Timer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!stopped)
        {
            time += Time.deltaTime;
            timerText.text = FormatTime(time);
        }
    }

    public void StartTimer()
    {
        stopped = false;
    }

    public void StopTimer()
    {
        stopped = true;
    }

    public static string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        string seconds = string.Format("{0:0.00}", time - minutes * 60);
        return minutes + ":" + seconds;
    }
}
