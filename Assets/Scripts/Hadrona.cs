using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class Hadrona : MonoBehaviour
{
    private static Stack<Hadrona> moveOrder = new Stack<Hadrona>();

    private Stack<Vector3Int> moves = new Stack<Vector3Int>();
    private float step = 1.28f;
    private Light2D selectLight;

    [SerializeField] private Tile trailTile;
    private Tilemap obstacles;
    public Color Color { get; private set; }
    public static Hadrona ActiveHadrona => ActiveHadronaManager.Instance.ActiveHadrona;

    private void Awake()
    {
        Color = ColorManager.GetHadronaColor();
        selectLight = GetComponent<Light2D>();
        obstacles = GameObject.Find("Obstacles").GetComponent<Tilemap>();
        selectLight.color = Color;
        selectLight.enabled = false;
        step = obstacles.cellSize.x;
        var particleSettings = GetComponentInChildren<ParticleSystem>().main;
        particleSettings.startColor = new ParticleSystem.MinMaxGradient(Color);
        particleSettings.startSize = Camera.main.orthographicSize / 100;
    }

    private void GenerateTrail()
    {
        Vector3Int cellPos = obstacles.WorldToCell(transform.position);
        GenerateTrail(cellPos);
    }

    private void GenerateTrail(Vector3Int cellPos)
    {
        trailTile.color = Color;
        obstacles.SetTile(cellPos, trailTile);
    }

    public void Move(Vector3Int direction)
    {
        if (direction.magnitude < 1)
            return;

        Vector3Int cellPos = obstacles.WorldToCell(transform.position);
        var obstacle = obstacles.GetTile(cellPos + direction);

        if (obstacle is object)
            return;
        
        GenerateTrail(cellPos + direction);
        transform.Translate((Vector3) direction * step);
        moveOrder.Push(this);
        moves.Push(cellPos);
    }

    private void OnMouseDown()
    {
        ActiveHadronaManager.Instance.ActivateHadrona(this);
    }

    public void Select()
    {
        if (ActiveHadrona != null)
            ActiveHadrona.selectLight.enabled = false;

        selectLight.enabled = true;
        GenerateTrail();
    }

    public static void UndoMove()
    {
        if (moveOrder.Count == 0)
            return;
        Hadrona h = moveOrder.Pop();
        if (h.moves.Count == 0)
            return;
        h.Select();
        Vector3Int cellPos = h.obstacles.WorldToCell(h.transform.position);
        Vector3Int movement = h.moves.Pop() - cellPos;
        h.obstacles.SetTile(cellPos, null);
        h.transform.Translate((Vector3)movement * h.step);
    }
}