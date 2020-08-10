using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Hadrona hadrona;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool finished = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (hadrona is Hadrona)
        {
            Color c = hadrona.Color;
            c.a = 50;
            spriteRenderer.color = c;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!finished && other.CompareTag("Player"))
        {
            Hadrona hadrona = other.GetComponent<Hadrona>();
            if (this.hadrona is null || hadrona.Equals(this.hadrona))
            {
                ActiveHadronaManager.Instance.DeleteHadrona();
                finished = true;
                audioSource.Play();
            }
        }
    }
}
