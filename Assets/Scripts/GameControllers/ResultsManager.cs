using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    [SerializeField]
    private LevelStats results;
    public static ResultsManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void FinishLevel()
    {
        results.SaveResult(SceneManager.GetActiveScene().buildIndex, Timer.Instance.time);
    }

    public float GetResult(int buildIndex)
    {
        return results.GetResult(buildIndex);
    }
}
