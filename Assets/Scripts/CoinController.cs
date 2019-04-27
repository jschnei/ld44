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

    public Sprite FIVE_SPRITE;
    public Sprite TEN_SPRITE;
    public Sprite TWENTYFIVE_SPRITE;


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

        Vector2 position = rigidbody2d.position;
        position.x += 3.0f * horizontal * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void Activate()
    {
        Debug.Log("Activating!");
        activeCoin = true;
    }

    public void Deactivate()
    {
        Debug.Log("Deactivating!");
        activeCoin = false;
    }
}
