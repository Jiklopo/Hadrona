using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private Animator animator;
    private int nextSceneNumber;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void NextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadLevel(int index)
    {
        nextSceneNumber = index;
        animator.SetTrigger("FadeOut");
    }

    public void OnAnimationEnd()
    {
        SceneManager.LoadScene(nextSceneNumber, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}