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
    Rigidbody2D rigidbody2d;
    public bool activeCoin = false;
    public CoinType coinType = CoinType.TEN;

    public float horizontalSpeed = 50.0f;
    public float jumpStrength = 100.0f;

    public Sprite FIVE_SPRITE;
    public Sprite TEN_SPRITE;
    public Sprite TWENTYFIVE_SPRITE;

    GameObject groundedOn = null;
    bool isGrounded = false;

    const float COLLISION_GROUND_NORMAL_THRESHOLD = 0.9f;

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

        Transform spriteChild = this.transform.Find("sprite");
        SpriteRenderer spriteRenderer = spriteChild.GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();

        if (coinType == CoinType.FIVE) {
            spriteRenderer.sprite = FIVE_SPRITE;
            boxCollider.size = new Vector2(1.5f, 1.5f);
        } else if (coinType == CoinType.TEN) {
            spriteRenderer.sprite = TEN_SPRITE;
            boxCollider.size = new Vector2(1.0f, 1.0f);
        } else if (coinType == CoinType.TWENTYFIVE) {
            spriteRenderer.sprite = TWENTYFIVE_SPRITE;
            boxCollider.size = new Vector2(2.0f, 2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeCoin) return;

        float horizontal = Input.GetAxis("Horizontal");

        Vector2 velocity = rigidbody2d.velocity;

        velocity.x = horizontal * horizontalSpeed;

        rigidbody2d.velocity = velocity;

        if (isGrounded && Input.GetKeyDown("up"))
        {
            Debug.Log("jump!");
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
}
