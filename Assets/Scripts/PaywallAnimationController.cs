using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaywallAnimationController : MonoBehaviour
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
        animator.SetTrigger("OpenDoor");
        boxCollider.enabled = false;
    }
}
