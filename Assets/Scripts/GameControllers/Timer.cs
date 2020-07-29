using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float time = 0;
    
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
            int minutes = (int)time / 60;
            string seconds = string.Format("{0:0.00}", time - minutes * 60);
            timerText.text = minutes + ":" + seconds;
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
}
