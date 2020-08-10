using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] float minAnimationSpeed = .7f;
    [SerializeField] float maxAnimationSpeed = 1.3f;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("SpinSpeed", Random.Range(minAnimationSpeed, maxAnimationSpeed));
    }
}
