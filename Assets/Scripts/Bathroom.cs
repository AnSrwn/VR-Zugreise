using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    AudioManager audioManager;
    SceneManager sceneManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        sceneManager = FindObjectOfType<SceneManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioManager.PlaySet(AudioManager.MusicSet.Bathroom);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (sceneManager.sceneNumber == 0)
            {
                audioManager.PlaySet(AudioManager.MusicSet.Woods);
            } else if (sceneManager.sceneNumber == 1)
            {
                audioManager.PlaySet(AudioManager.MusicSet.Conductor);
            }
        }
    }
}
