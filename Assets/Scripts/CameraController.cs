using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 delta = target.transform.position - this.transform.position;
        delta.z = 0f;
        if (delta.magnitude > 0.05) {
            if (delta.magnitude > 2) {
                delta = Vector3.ClampMagnitude(delta, 2f);
            }
            this.transform.position += delta * 0.1f;
        }
    }

    public void FocusTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
