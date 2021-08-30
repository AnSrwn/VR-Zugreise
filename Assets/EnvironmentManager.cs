using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class EnvironmentManager : MonoBehaviour
{
    public hightmapTerrain terrain;
    public GameObject skyObject;
    public GameObject houseSpawner;
    public GameObject secondSceneInteractables;

    private PhysicallyBasedSky sky;

    void Start()
    {
        Volume vol = skyObject.GetComponent<Volume>();
        vol.profile.TryGet<PhysicallyBasedSky>(out sky);
    }

    public void LoadOcean()
    {
        StartCoroutine(OceanCoroutine());
    }

    IEnumerator OceanCoroutine()
    {
        houseSpawner.SetActive(true);
        secondSceneInteractables.SetActive(true);
        sky.groundTint.Override(new Color(0.25f, 0.375f, 0.5f));
        terrain.showtrees = false;
        terrain.ocean = true;
        yield return 0;
        terrain.ocean = false;
        yield return 0;
        terrain.ocean = true;
        yield return 0;
        terrain.movingHeightMap = false;
    }
}
