using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] Slider zoomSlider;
    [SerializeField] float maxCameraSize = 18f, minCameraSize = 5f;
    [SerializeField] float maxY = 5f, minY = -5f;
    [SerializeField] float maxX = 10f, minX = -10f;
    private float MinX => minX + camera.orthographicSize / 2;
    private float MaxX => maxX - camera.orthographicSize / 2;
    private float MinY => minY + camera.orthographicSize / 2;
    private float MaxY => maxY - camera.orthographicSize / 2;
    private GraphicRaycaster raycaster;
    private bool isMobile;
    private Vector2 lastMousePos;
    private void Awake()
    {
        if (camera == null)
            camera = Camera.main;
        raycaster = GetComponentInChildren<GraphicRaycaster>();
        zoomSlider.maxValue = maxCameraSize;
        zoomSlider.minValue = minCameraSize;
        isMobile = Application.isMobilePlatform;
    }

    private void Start()
    {
        zoomSlider.value = maxCameraSize;
    }

    void Update()
    {
        Vector2 translation = Vector2.zero;
        if (isMobile)
            translation = GetMobileTranslation();
        if (!isMobile && Input.GetMouseButtonDown(0))
            lastMousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (!isMobile && Input.GetMouseButton(0))
            translation = GetPCTranslation(Input.mousePosition);

        if (translation.Equals(Vector2.zero))
            return;
        
        camera.transform.position = new Vector3(
                Mathf.Clamp(camera.transform.position.x + translation.x, MinX, MaxX),
                Mathf.Clamp(camera.transform.position.y + translation.y, MinY, MaxY),
                camera.transform.position.z
                );
    }

    private Vector2 GetMobileTranslation()
    {
        int touchCount = Input.touchCount;
        if (touchCount == 0)
            return Vector2.zero;

        Touch touch = Input.GetTouch(0); 

        if (RaycastAnyone(touch.position))
            return Vector2.zero;

        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 curPos = camera.ScreenToWorldPoint(touch.position);
            Vector2 prevPos = camera.ScreenToWorldPoint(touch.position - touch.deltaPosition);
            Vector2 difference = prevPos - curPos;
            return difference;
        }
        return Vector2.zero;
    }

    private Vector2 GetPCTranslation(Vector2 position)
    {
        Vector2 curPos = camera.ScreenToWorldPoint(position);
        Vector2 difference = lastMousePos - curPos;
        lastMousePos = curPos;
        return difference;
    }

    private bool RaycastAnyone(Vector3 position)
    {
        PointerEventData eventData = new PointerEventData(null);
        eventData.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        return results.Count != 0;
    }
    public void OnSliderValueChange()
    {
        camera.orthographicSize = zoomSlider.value;
    }
}
