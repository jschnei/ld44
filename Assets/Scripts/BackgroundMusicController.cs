using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance = null;
    public static BackgroundMusicController Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            AudioSource backgroundMusic = gameObject.GetComponent<AudioSource>();
            if (backgroundMusic.mute)
            {
                backgroundMusic.mute = false;
            }
            else
            {
                backgroundMusic.mute = true;
            }
        }
    }
}
