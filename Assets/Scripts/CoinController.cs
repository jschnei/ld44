using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Sprite FIVE_SPRITE;
    public Sprite TEN_SPRITE;
    public Sprite TWENTYFIVE_SPRITE;

    Rigidbody2D rigidbody2d;

    GameObject groundedOn = null;
    bool isGrounded = false;
    bool activeCoin = false;

    CoinsManager manager;

    const float COLLISION_GROUND_NORMAL_THRESHOLD = 0.7f;

    void OnCollisionEnter2D(Collision2D collision)
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

        Transform spriteChild = this.transform.Find("sprite");
        SpriteRenderer spriteRenderer = spriteChild.GetComponent<SpriteRenderer>();

        if (coinType == CoinType.FIVE) {
            spriteRenderer.sprite = FIVE_SPRITE;
        } else if (coinType == CoinType.TEN) {
            spriteRenderer.sprite = TEN_SPRITE;
        } else if (coinType == CoinType.TWENTYFIVE) {
            spriteRenderer.sprite = TWENTYFIVE_SPRITE;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
