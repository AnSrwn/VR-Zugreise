using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker
{
    public string name;
    public GameObject gameObject;
    public AudioSource audioSource;

    public Speaker(string name, GameObject gameObject, AudioSource audioSource)
    {
        this.name = name;
        this.gameObject = gameObject;
        this.audioSource = audioSource;
    }
}
