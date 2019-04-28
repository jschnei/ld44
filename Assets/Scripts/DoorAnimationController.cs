using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        Debug.Log("Opening Door!");
        animator.SetTrigger("OpenDoor");
        boxCollider.enabled = false;
    }

    public void CloseDoor()
    {
        Debug.Log("Closing Door!");

        if (animator == null) return;

        animator.SetTrigger("CloseDoor");
        boxCollider.enabled = true;
    }
}
