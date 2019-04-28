using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    GameObject upButton;
    GameObject downButton;

    DoorAnimationController buttonDoor;
    // Start is called before the first frame update

    int weightOnButton = 0;
    bool isPressed = false;

    void Start()
    {
        upButton = transform.Find("ButtonUp").gameObject;
        downButton = transform.Find("ButtonDown").gameObject;

        buttonDoor = gameObject.GetComponentInParent<DoorAnimationController>();

        ReleaseButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PressButton()
    {
        upButton.GetComponent<SpriteRenderer>().enabled = false;
        upButton.GetComponent<BoxCollider2D>().enabled = false;

        downButton.GetComponent<SpriteRenderer>().enabled = true;

        buttonDoor.OpenDoor();

        isPressed = true;
    }

    void ReleaseButton()
    {
        upButton.GetComponent<SpriteRenderer>().enabled = true;
        upButton.GetComponent<BoxCollider2D>().enabled = true;

        downButton.GetComponent<SpriteRenderer>().enabled = false;

        buttonDoor.CloseDoor();

        isPressed = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Button triggered!");

        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        weightOnButton += coin.GetValue();

        if (!isPressed)
        {
            PressButton();
        }
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Button released!");
        CoinController coin = collider.gameObject.GetComponent<CoinController>();
        if (coin == null) return;

        weightOnButton -= coin.GetValue();
        Debug.Log(weightOnButton);
        if (isPressed && weightOnButton == 0)
        {
            Debug.Log("Releasing Button");
            ReleaseButton();
        }
    }
}
