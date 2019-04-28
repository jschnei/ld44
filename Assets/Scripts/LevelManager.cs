using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    CameraController mainCamera;
    CoinsManager player;

    // Use Awake because the camera is called in CoinsManager's Start, so the camera needs to be initialized first.
    void Awake()
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
        mainCamera.FocusTarget(target);
    }
}
