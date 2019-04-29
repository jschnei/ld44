using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;

    public AudioClip openDoorAudio;
    public AudioClip closeDoorAudio;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        Debug.Log("Opening Door!");
        this.GetComponent<AudioSource>().PlayOneShot(openDoorAudio, 0.5f);
        animator.SetTrigger("OpenDoor");
        boxCollider.enabled = false;
    }

    public void CloseDoor()
    {
        Debug.Log("Closing Door!");
        this.GetComponent<AudioSource>().PlayOneShot(closeDoorAudio, 0.5f);

        if (animator == null) return;

        animator.SetTrigger("CloseDoor");
        boxCollider.enabled = true;
    }
}
