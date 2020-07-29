using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static GameMenu Instance { get; private set; }
    [SerializeField]private Canvas menuCanvas;

    private void Awake()
    {
        Instance = this;
        menuCanvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }

    public void ToggleMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        menuCanvas.gameObject.SetActive(!menuCanvas.gameObject.activeSelf);
    }

    public void QuitToMenu()
    {
        LevelManager.Instance.LoadLevel(1);
    }

    public void Restart()
    {
        LevelManager.Instance.RestartLevel();
    }
}