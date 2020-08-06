using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float maxCameraSize = 18f, minCameraSize = 5f;
    [SerializeField] float maxY = 5f, minY = -5f;
    [SerializeField] float maxX = 10f, minX = -10f;
    private float MinX => minX + camera.orthographicSize / 2;
    private float MaxX => maxX - camera.orthographicSize / 2;
    private float MinY => minY + camera.orthographicSize / 2;
    private float MaxY => maxY - camera.orthographicSize / 2;
    private GraphicRaycaster raycaster;
    private void Awake()
    {
        if (camera == null)
            camera = Camera.main;
        raycaster = GetComponentInChildren<GraphicRaycaster>();
        camera.orthographicSize = maxCameraSize;
    }

    void Update()
    {
        int touchCount = Input.touchCount;
        if (touchCount == 1)
            Move();
        else if (touchCount == 2)
            Zoom();
    }


    private void Move()
    {
        Touch touch = Input.GetTouch(0);

        if (RaycastAnyone(touch.position))
            return;

        Vector2 curPos = camera.ScreenToWorldPoint(touch.position);
        Vector2 prevPos = camera.ScreenToWorldPoint(touch.position - touch.deltaPosition);
        Vector2 difference = prevPos - curPos;

        camera.transform.position = new Vector3(
                Mathf.Clamp(camera.transform.position.x + difference.x, MinX, MaxX),
                Mathf.Clamp(camera.transform.position.y + difference.y, MinY, MaxY),
                camera.transform.position.z
                );
    }

    private void Zoom()
    {
        Touch t0 = Input.GetTouch(0);
        Touch t1 = Input.GetTouch(1);

        Vector2 prevT0Pos = t0.position - t0.deltaPosition;
        Vector2 prevT1Pos = t1.position - t1.deltaPosition;

        float distance = camera.ScreenToWorldPoint(t0.position - t1.position).magnitude;
        float prevDistance = camera.ScreenToWorldPoint(prevT0Pos - prevT1Pos).magnitude;

        camera.orthographicSize -= (distance - prevDistance);
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minCameraSize, maxCameraSize);
    }

    private bool RaycastAnyone(Vector3 position)
    {
        PointerEventData eventData = new PointerEventData(null);
        eventData.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        return results.Count != 0;
    }
}
