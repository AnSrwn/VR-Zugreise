using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public int sceneNumber = 0;
    public GameObject dude;
    public GameObject attendant;

    public GameObject conductor;
    public GameObject child;

    void Update()
    {
        switch(sceneNumber)
        {
            case 0:
            dude.SetActive(true);
            attendant.SetActive(true);
            conductor.SetActive(false);
            child.SetActive(false);
            break;

            case 1:
            dude.SetActive(false);
            attendant.SetActive(false);
            conductor.SetActive(true);
            child.SetActive(false);
            break;
        }
    }
}
