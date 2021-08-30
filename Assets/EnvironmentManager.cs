using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject terrain;
    public hightmapTerrain terrainHightmap;
    public GameObject skyObject;
    public GameObject houseSpawner;
    public GameObject secondSceneInteractables;
    public bool waterRising = false;
    public GameObject space;

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

    public void RiseOcean()
    {
        StartCoroutine(RiseCoroutine());
    }

    IEnumerator OceanCoroutine()
    {
        houseSpawner.SetActive(true);
        secondSceneInteractables.SetActive(true);
        sky.groundTint.Override(new Color(0.25f, 0.375f, 0.5f));
        terrainHightmap.showtrees = false;
        terrainHightmap.ocean = true;
        yield return 0;
        terrainHightmap.ocean = false;
        yield return 0;
        terrainHightmap.ocean = true;
        yield return 0;
        terrainHightmap.movingHeightMap = false;
    }

    IEnumerator RiseCoroutine()
    {
        waterRising = true;
        while(terrainHightmap.gameObject.transform.position.y < Camera.main.transform.position.y - 1.6f)
        {
            Vector3 terrainPosition = terrainHightmap.gameObject.transform.position;
            float newY = terrainPosition.y + 0.0005f / Time.deltaTime;
            terrainHightmap.gameObject.transform.position = new Vector3(terrainPosition.x, newY, terrainPosition.z);
            yield return null;
        }
        waterRising = false;
    }
    public IEnumerator LoadSpace(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        houseSpawner.SetActive(false);
        terrain.SetActive(false);
        space.SetActive(true);
    }
}
