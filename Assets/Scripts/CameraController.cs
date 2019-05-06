using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    Camera myCamera;
    SpriteRenderer boundingBox;

    public float leftBound = -100.0f;
    public float rightBound = 100.0f;
    public float topBound = -100.0f;
    public float bottomBound = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = gameObject.GetComponent<Camera>();
        boundingBox = gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("p"))
        {

            // double aspectRatio = Screen.width / Screen.height;
            var lowerLeft = myCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            var upperRight = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            Debug.Log("camera center: " + transform.position);
            Debug.Log("camera lower left: " + lowerLeft);
            Debug.Log("camera upper right: " + upperRight);
            Debug.Log("camera size: " + (upperRight - lowerLeft));
            // Debug.Log("camera vert size: " + myCamera.orthographicSize);
            // Debug.Log("camera hor size: " + (myCamera.orthographicSize * aspectRatio));
            Debug.Log("bounding box center: " + boundingBox.transform.position);
            Debug.Log("bounding box: " + (boundingBox.transform.position - Vector3.Scale(boundingBox.sprite.bounds.size, boundingBox.transform.localScale) / 2.0f));
            // Debug.Log("bounding box scale: " + boundingBox.transform.localScale);
        }*/

        if (target == null) return;

        Vector3 delta = target.transform.position - transform.position;
        delta.z = 0f;
        if (delta.magnitude > 0.05) {
            if (delta.magnitude > 2) {
                delta = Vector3.ClampMagnitude(delta, 2f);
            }
            transform.position += delta * 0.1f;
        }

        Vector3 lowerLeft = myCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector3 cameraSize = (upperRight - lowerLeft) / 2.0f;
        Vector3 boundsSize = Vector3.Scale(boundingBox.sprite.bounds.size, boundingBox.transform.localScale) / 2.0f;

        float leftBound = boundingBox.transform.position.x - boundsSize.x + cameraSize.x;
        float rightBound = boundingBox.transform.position.x + boundsSize.x - cameraSize.x;

        float topBound = boundingBox.transform.position.y - boundsSize.y + cameraSize.y;
        float bottomBound = boundingBox.transform.position.y + boundsSize.y - cameraSize.y;

        Vector3 pos = transform.position;
        if (leftBound < rightBound) pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        if (topBound < bottomBound) pos.y = Mathf.Clamp(pos.y, topBound, bottomBound);

        transform.position = pos;
    }

    public void FocusTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
