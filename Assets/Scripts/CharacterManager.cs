using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject dude;
    public GameObject attendant;

    public GameObject conductor;
    public GameObject child;
    public GameObject npcMan;
    public GameObject npcWoman;

    public GameObject conductorPositionSecondScene;

    public void startFirstScene()
    {
        dude.SetActive(true);
        attendant.SetActive(true);
        conductor.SetActive(true);
        child.SetActive(true);
        npcMan.SetActive(true);
        npcWoman.SetActive(true);
    }

    public void startSecondScene()
    {
        dude.SetActive(false);
        attendant.SetActive(false);

        conductor.SetActive(true);
        conductor.transform.position = conductorPositionSecondScene.transform.position;
        conductor.transform.rotation = conductorPositionSecondScene.transform.rotation;
        conductor.GetComponent<Conductor>().playIdleAnimation();

        child.SetActive(false);
        npcMan.SetActive(false);
        npcWoman.SetActive(false);
    }

    public void startThirdScene()
    {
        dude.SetActive(false);
        attendant.SetActive(false);
        conductor.SetActive(false);
        child.SetActive(true);
        npcMan.SetActive(false);
        npcWoman.SetActive(false);
    }
}
