﻿using System.Collections;
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

    public float polygonColliderScaleFactor = 1.0f;

    Rigidbody2D rigidbody2d;

    GameObject groundedOn;
    bool isGrounded = false;
    bool activeCoin = false;
    bool movable = false;

    CoinsManager manager;

    private float initialX;

    const float COLLISION_GROUND_NORMAL_THRESHOLD = 0.7f;

    PayslotController activePayslot;
    GameObject activePayslotUI;

    public AudioClip jumpAudio;
    public AudioClip coinCollideAudio;
    public AudioClip landAudio;
    public AudioClip depositAudio;
    public AudioClip tinkAudio;

    LevelManager level;

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.transform.name);
        if (collision.gameObject.tag == "coin") {
            this.GetComponent<AudioSource>().PlayOneShot(coinCollideAudio, 0.3f);
        } else if (collision.gameObject.transform.name == "Walls/Floors") {
            this.GetComponent<AudioSource>().PlayOneShot(landAudio, 0.5f);
        } else if (collision.gameObject.transform.name.Contains("Door")) {
            this.GetComponent<AudioSource>().PlayOneShot(tinkAudio, 0.5f);
        }
    }

    // Note that this is called separately for each object that it collides with.
    void OnCollisionStay2D(Collision2D collision)
    {
    	if (groundedOn != null && groundedOn != collision.gameObject) {
    		return;
    	}

    	bool foundContact = false;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > COLLISION_GROUND_NORMAL_THRESHOLD)
            {
                isGrounded = true;
                groundedOn = collision.gameObject;
                foundContact = true;
                break;
            }
        }

        if (!foundContact) {
        	isGrounded = false;
        	groundedOn = null;
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

    void ScalePolygonCollider(float scaleFactor) {
    	PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
    	Debug.Assert(collider.pathCount == 1, "polygon collider should only be a single polygon");
    	Vector2[] path = collider.GetPath(0);
    	for (int i = 0; i < path.Length; i++) {
    		path[i] = path[i]*scaleFactor;
    	}
    	collider.SetPath(0, path);
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

        ScalePolygonCollider(polygonColliderScaleFactor);

        initialX = transform.position.x;

        activePayslotUI = transform.Find("ActivePayslotUI").gameObject;
        activePayslotUI.SetActive(true);

        level = GameObject.FindGameObjectWithTag("level").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform) {
            if (child.name == "ActivePayslotUI") continue;

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

        // use this if you don't want any movement to be possible until movable
        // if (!activeCoin || !movable) {
        if (!activeCoin || level.won) {
        	return;
        }

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0) {
	        Vector2 velocity = rigidbody2d.velocity;
	        velocity.x = horizontal * horizontalSpeed;
	        rigidbody2d.velocity = velocity;
		}
		
        if (isGrounded && Input.GetKeyDown("up")) {
            this.GetComponent<AudioSource>().PlayOneShot(jumpAudio, 0.5f);
        	Debug.Log("attempting to jump");
            rigidbody2d.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }

        if(Input.GetKeyDown("down") && activePayslot != null && movable)
        {
            Debug.Log("paying coin!");
            activePayslot.ProcessCoin(this);
        }
    }

    public void Activate()
    {
        activeCoin = true;

        movable = false;
        StartCoroutine(SlowActivate());

        ReconfigureUI();
    }

    IEnumerator SlowActivate()
    {
        yield return new WaitForSeconds(0.2f);
        movable = true;
    }

    public void Deactivate()
    {

        activeCoin = false;
        ReconfigureUI();
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

    public PayslotController ActivePayslot
    {
        get => activePayslot;
    }

    public void ReconfigureUI()
    {
        if (activePayslotUI == null) return;

        if(activePayslot != null && activeCoin && activePayslot.IsActive())
        {
            activePayslotUI.GetComponent<SpriteRenderer>().enabled = true;   
        }
        else
        {
            activePayslotUI.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void AttachPayslot(PayslotController payslot)
    {
        Debug.Log("activate payslot");
        activePayslot = payslot;

        ReconfigureUI();
    }

    public void DetachPayslot()
    {
        Debug.Log("deactivate payslot");
        activePayslot = null;

        ReconfigureUI();
    }

}
