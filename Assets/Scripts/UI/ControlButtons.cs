using UnityEngine;

public class ControlButtons : MonoBehaviour
{
    public static ControlButtons Instance { get; private set; }
    public bool moveable = true;
    private void Awake()
    {
        Instance = this;
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
        if(moveable)
            ActiveHadronaManager.Instance.ActiveHadrona.Move(direction);
    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();
    }
}
