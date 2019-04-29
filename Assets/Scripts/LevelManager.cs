﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    CameraController mainCamera;
    CoinsManager player;

    public AudioClip levelCompleteAudio;

    public static string[] levelNames = { "MainScene",
                                          "AndersonLevel" };

    int levelIndex;
    Scene scene;

    // Use Awake because the camera is called in CoinsManager's Start, so the camera needs to be initialized first.
    void Awake()
    {
        mainCamera = gameObject.GetComponentInChildren<CameraController>();
        player = gameObject.GetComponentInChildren<CoinsManager>();

        scene = SceneManager.GetActiveScene();
        levelIndex = System.Array.IndexOf(levelNames, scene.name);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(scene.name);
        }
    }

    public void FocusCamera(GameObject target)
    {
        if (target != null) {
            mainCamera.FocusTarget(target);
        }
    }

    public void WinLevel()
    {
        Debug.Log("Level won!");
        this.GetComponent<AudioSource>().PlayOneShot(levelCompleteAudio, 0.5f);
        string nextLevel = levelNames[(levelIndex + 1) % levelNames.Length];
        Debug.Log(nextLevel);
        StartCoroutine(GoToNextLevel(nextLevel));
    }

    IEnumerator GoToNextLevel(string nextLevel) {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextLevel);
    }
}
