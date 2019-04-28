using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    CameraController mainCamera;
    CoinsManager player;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponentInChildren<CameraController>();
        player = gameObject.GetComponentInChildren<CoinsManager>();
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void FocusCamera(GameObject target)
    {
        if (target != null) {
            mainCamera.FocusTarget(target);
        }
    }
}
