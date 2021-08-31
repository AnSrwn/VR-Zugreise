using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public GameObject cow;
    public GameObject rocket;
    public GameObject aliens;
    public GameObject astronaut;
    public GameObject stars;
    public GameObject astroSpawner;
    public GameObject solarSystem;
    public GameObject saturn;
    public Material glowingCow;
    public Material glowingRocket;
    public Material glowingPlanet;
    public Material glowingAstronaut;
    public Material noStars;

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

    public void LightCow()
    {
        Material[] mats = cow.GetComponentInChildren<MeshRenderer>().materials;
        mats[0] = mats[1] = mats[2] = glowingCow;
        cow.GetComponentInChildren<MeshRenderer>().materials = mats;
    }

    public void StartRocket()
    {
        rocket.SetActive(true);
    }

    public void DestroyRocket()
    {
        rocket.SetActive(false);
    }

    public void LightRocket()
    {
        Material[] mats = rocket.GetComponentInChildren<MeshRenderer>().materials;
        mats[1] = glowingRocket;
        rocket.GetComponentInChildren<MeshRenderer>().materials = mats;
    }

    public void StartAliens()
    {
        aliens.SetActive(true);
    }

    public void DestroyAliens()
    {
        aliens.SetActive(false);
    }

    public void LightAliens()
    {
        Material[] mats = aliens.GetComponentInChildren<MeshRenderer>().materials;
        mats[0] = glowingPlanet;
        aliens.GetComponentInChildren<MeshRenderer>().materials = mats;
    }

    public void StartAstronaut()
    {
        astronaut.SetActive(true);
    }

    public void DestroyAstronaut()
    {
        astronaut.SetActive(false);
    }

    public void LightAstronaut()
    {
        Material[] mats = astronaut.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        mats[1] = glowingAstronaut;
        astronaut.GetComponentInChildren<SkinnedMeshRenderer>().materials = mats;
    }

    public void ClearFantasy()
    {
        Debug.Log("Clear fantasy");
        cow.SetActive(false);
        rocket.SetActive(false);
        aliens.SetActive(false);
        astronaut.SetActive(false);
    }

    public void ClearAll()
    {
        Debug.Log("Clear all");
        ClearFantasy();
        Material[] mats = stars.GetComponentInChildren<MeshRenderer>().materials;
        mats[0] = noStars;
        stars.GetComponentInChildren<MeshRenderer>().materials = mats;
        astroSpawner.SetActive(false);
        solarSystem.SetActive(false); ;
        saturn.SetActive(false);
}
}
