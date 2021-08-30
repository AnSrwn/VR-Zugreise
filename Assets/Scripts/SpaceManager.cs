using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public GameObject cow;
    public GameObject rocket;
    public GameObject aliens;
    public GameObject astronaut;

    private void Start() {
        cow.SetActive(false);
        rocket.SetActive(false);
        aliens.SetActive(false);
        astronaut.SetActive(false);
    }

    public void StartCow()
    {
        cow.SetActive(true);
    }

    public void DestroyCow()
    {
        cow.SetActive(false);
    }

    public void StartRocket()
    {
        rocket.SetActive(true);
    }

    public void DestroyRocket()
    {
        rocket.SetActive(false);
    }

    public void StartAliens()
    {
        aliens.SetActive(true);
    }

    public void DestroyAliens()
    {
        aliens.SetActive(false);
    }

    public void StartAstronaut()
    {
        astronaut.SetActive(true);
    }

    public void DestroyAstronaut()
    {
        astronaut.SetActive(false);
    }
}
