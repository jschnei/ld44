using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CoinType {
    FIVE,
    TEN,
    TWENTYFIVE,
}

public class CoinController : MonoBehaviour
{
    public CoinType coinType = CoinType.TEN;

    public float horizontalSpeed = 50.0f;
    public float jumpStrength = 100.0f;

    Rigidbody2D rigidbody2d;

    GameObject groundedOn = null;
    bool isGrounded = false;
    bool activeCoin = false;

    CoinsManager manager;

    private float initialX;

    const float COLLISION_GROUND_NORMAL_THRESHOLD = 0.7f;

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > COLLISION_GROUND_NORMAL_THRESHOLD)
            {
                isGrounded = true;
                groundedOn = collision.gameObject;
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == groundedOn)
        {
            groundedOn = null;
            isGrounded = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        manager = gameObject.GetComponentInParent<CoinsManager>();

        if (coinType == CoinType.FIVE) {
            foreach (Transform child in this.transform) {
                child.gameObject.SetActive(child.name.Contains("Nickel"));
            }
        } else if (coinType == CoinType.TEN) {
            foreach (Transform child in this.transform) {
                child.gameObject.SetActive(child.name.Contains("Dime"));
            }
        } else if (coinType == CoinType.TWENTYFIVE) {
            foreach (Transform child in this.transform) {
                child.gameObject.SetActive(child.name.Contains("Quarter"));
            }
        }

        initialX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform) {
            child.GetComponent<CoinRenderingHelper>().eyesOpen = activeCoin;
            float r = 0f;
            if (coinType == CoinType.FIVE) {
                r = 1.5f;
            } else if (coinType == CoinType.TEN) {
                r = 1f;
            } else if (coinType == CoinType.TWENTYFIVE) {
                r = 2f;
            }
            child.GetComponent<CoinRenderingHelper>().rotation = (float) ((this.transform.position.x - initialX) / r * -180.0 / Math.PI);
        }

        if (!activeCoin) {
        	return;
        }

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0) {
	        Vector2 velocity = rigidbody2d.velocity;
	        velocity.x = horizontal * horizontalSpeed;
	        rigidbody2d.velocity = velocity;
		}
		
        if (isGrounded && Input.GetKeyDown("up"))
        {
            rigidbody2d.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }

    }

    public void Activate()
    {
        activeCoin = true;
    }

    public void Deactivate()
    {

        activeCoin = false;
    }

    public int GetValue()
    {
        switch (coinType)
        {
            case CoinType.FIVE:
                return 5;
            case CoinType.TEN:
                return 10;
            case CoinType.TWENTYFIVE:
                return 25;
            default:
                Debug.LogError("Invalid coin type");
                return 0;
        }
    }

    // Gets index of coin into CoinsManager's children
    public int GetCoinsIndex()
    {
        return gameObject.transform.GetSiblingIndex();
    }

    public void DestroyCoin()
    {
        manager.DestroyCoin(this);
    }

    public void SetLocation(Vector2 position)
    {
        transform.position = position;
    }

}
