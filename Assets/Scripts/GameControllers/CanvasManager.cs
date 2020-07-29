using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private void Awake()
    {
        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            c.renderMode = RenderMode.ScreenSpaceCamera;
            c.worldCamera = Camera.main;
        }
    }
}
