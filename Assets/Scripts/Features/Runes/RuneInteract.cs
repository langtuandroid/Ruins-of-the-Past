using UnityEngine;

public class RuneInteract : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isTrigger;
    private int trueCounter;
    private bool isFox;
    private bool isWolf;

    private void Awake()
    {
        animator.enabled = false;
    }

    private void Update()
    {
        if (!isTrigger || trueCounter != 2) return;
        if ((!Input.GetKeyDown(KeyCode.S) || !isFox) && (!Input.GetKeyDown(KeyCode.DownArrow) || !isWolf)) return;
        animator.enabled = true;
        animator.Play("Rockslide");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fox"))
            isFox = true;
        
        if (other.CompareTag("Wolf"))
            isWolf = true;
        
        isTrigger = true;
        trueCounter = Mathf.Min(trueCounter + 1, 2);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fox"))
            isFox = false;
        
        if (other.CompareTag("Wolf"))
            isWolf = false;

        trueCounter = 1;
        isTrigger = false;
    }
}
