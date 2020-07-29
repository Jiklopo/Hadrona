using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStart : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber = 1;
    void Start()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
