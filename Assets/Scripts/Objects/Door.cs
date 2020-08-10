using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : Activateable
{
    private Vector3Int cellPos;
    private Tilemap obstacles;
    private SpriteRenderer renderer;
    private AudioSource audio;
    public bool Enabled { get; private set; }
    [SerializeField] private Tile closedDoor;

    private void Awake()
    {
        Enabled = false;
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        obstacles = GameObject.Find("Obstacles").GetComponent<Tilemap>();
    }

    private void Start()
    {
        cellPos = obstacles.WorldToCell(transform.position);
        Deactivate();
    }

    //Open the door
    public override void Activate()
    {
        renderer.enabled = true;
        obstacles.SetTile(cellPos, null);
        Enabled = true;
        audio.Play();
    }

    //Close the door
    public override void Deactivate()
    {
        renderer.enabled = false;
        obstacles.SetTile(cellPos, closedDoor);
        Enabled = false;
        audio.Play();
    }
}