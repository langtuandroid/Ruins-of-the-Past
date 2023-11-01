using UnityEngine;

public class RuneInteract : MonoBehaviour
{
    [SerializeField]
    private Animator animator; // reference to the Animator component
    [SerializeField]
    private AnimationClip clip;

    private void Awake()
    {
        animator.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (animator != null)
            {
                animator.enabled = true;
                animator.Play("Base Layer.Rockslide");
            }
        }
    }
}
