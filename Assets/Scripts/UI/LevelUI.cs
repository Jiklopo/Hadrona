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

    public void MoveHadronaX(int xMovement)
    {
        Move(new Vector3Int(xMovement, 0, 0));
    }

    public void MoveHadronaY(int yMovement)
    {
        Move(new Vector3Int(0, yMovement, 0));
    }

    private void Move(Vector3Int direction)
    {
        Hadrona.ActiveHadrona.Move(direction);
    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void UndoMove()
    {
        Hadrona.UndoMove();
    }
}
