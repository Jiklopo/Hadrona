using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float maxCameraSize = 15f, minCameraSize = 5f;
    [SerializeField] float maxY = 5, minY = -5;
    [SerializeField] float maxX = 10, minX = -10;
    [SerializeField] float denominator = 15f;
    private GraphicRaycaster raycaster;
    private void Awake()
    {
        if (camera == null)
            camera = Camera.main;
        raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        int touchCount = Input.touchCount;
        if (touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        PointerEventData eventData = new PointerEventData(null);
        eventData.position = touch.position;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        if (results.Count != 0)
            return;

        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 curPos = camera.ScreenToWorldPoint(touch.position);
            Vector2 prevPos = camera.ScreenToWorldPoint(touch.position - touch.deltaPosition);
            Vector2 difference = prevPos - curPos;
            camera.transform.position = new Vector3(
                Mathf.Clamp(camera.transform.position.x + difference.x, minX, maxX),
                Mathf.Clamp(camera.transform.position.y + difference.y, minY, maxY)
                );
        }
    }
}
