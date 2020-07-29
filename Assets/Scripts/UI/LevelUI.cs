using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    public static LevelUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void HideUI()
    {
        canvas.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        canvas.gameObject.SetActive(true);
    }

    public void ToggleMenu()
    {
        GameMenu.Instance.ToggleMenu();
    }

    public void UndoMove()
    {
        Hadrona.UndoMove();
    }
}
