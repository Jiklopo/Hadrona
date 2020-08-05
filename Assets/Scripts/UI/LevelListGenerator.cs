using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelListGenerator : MonoBehaviour
{
    [SerializeField] private LevelSelectButton buttonPrefab;
    void Start()
    {
        int levels = SceneManager.sceneCountInBuildSettings - 4;
        for (int i = 0; i < levels; i++)
        {
            LevelSelectButton button = Instantiate(buttonPrefab, transform);
            button.sceneIndex = i + 3;
            button.SetText((i+1).ToString());
            button.SetRecordText();
        }
    }
}
