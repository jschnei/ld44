using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public bool activeCoin = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
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
