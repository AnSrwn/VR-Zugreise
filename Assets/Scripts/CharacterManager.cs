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
    public GameObject npcMan;
    public GameObject npcWoman;

    public GameObject conductorPositionSecondScene;

    void Update()
    {
        switch(sceneNumber)
        {
            case 0:
            dude.SetActive(true);
            attendant.SetActive(true);
            conductor.SetActive(true);
            child.SetActive(true);
            npcMan.SetActive(true);
            npcWoman.SetActive(true);
            break;

            case 1:
            dude.SetActive(false);
            attendant.SetActive(false);

            conductor.SetActive(true);
            conductor.transform.position = conductorPositionSecondScene.transform.position;
            conductor.transform.rotation = conductorPositionSecondScene.transform.rotation;

            child.SetActive(false);
            npcMan.SetActive(false);
            npcWoman.SetActive(false);
             break;
        }
    }
}
