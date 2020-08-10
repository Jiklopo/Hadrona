using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : Activateable
{
    private Vector3Int cellPos;
    private Tilemap obstacles;
    private AudioSource audioSource;
    private Animator animator;
    public bool Enabled { get; private set; } = false;
    [SerializeField] private Tile closedDoor;

    private void Awake()
    {
        Enabled = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        obstacles = GameObject.Find("Obstacles").GetComponent<Tilemap>();
    }

    private void Start()
    {
        cellPos = obstacles.WorldToCell(transform.position);
        obstacles.SetTile(cellPos, closedDoor);
    }

    public override void Activate()
    {
        obstacles.SetTile(cellPos, null);
        animator.SetBool("Opened", true);
        Enabled = true;
        audioSource.Play();
    }

    public override void Deactivate()
    {
        obstacles.SetTile(cellPos, closedDoor);
        animator.SetBool("Opened", false);
        Enabled = false;
        audioSource.Play();
    }
}