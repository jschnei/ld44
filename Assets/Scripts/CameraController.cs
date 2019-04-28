using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    Vector3 offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        transform.position = target.transform.position + offsetZ;
    }

    public void FocusTarget(GameObject newTarget)
    {
        Debug.Log("Focusing camera!");

        offsetZ = new Vector3(0, 0, transform.position.z - newTarget.transform.position.z);
        target = newTarget;

    }
}
