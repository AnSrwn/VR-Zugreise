using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hightmapTerrain : MonoBehaviour
{

    public Texture2D[] hmap;
    public int depth = 20;
    public float scale = 1;

    public GameObject[] TreeList;
    public float threeSize = 1;
    public GameObject parent;
    public int animeSpeed = 5;
    int treecount = 0;
    public float offsetX = 0;
    public float offsetY = 0;
    public Texture2D[] test;
    private Terrain terrain;
    private SplatPrototype[] splatPrototype;
    public bool heyaa = true;
    public Texture2D[] ExtraTexture;
    public float xCoord;
    public float yCoord;

    void Start()
    {
        SetTerrainSplatMap(terrain, test);

    }

    // Start is called before the first frame update
    void Update()
    {

        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        if (heyaa)
        {
            SetTerrainSplatMap(terrain, test);
        }
        //SetTerrainSplatMap

        if (treecount <= 35)
        {

            treecount = 35;
        }
        if (treecount >= 35)
        {
            StartCoroutine(PlaceObject(50));
            offsetX += Time.deltaTime * animeSpeed;
            parent.transform.Translate(0 + -Time.deltaTime * animeSpeed, 0, 0);

        }



    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {

        float currentheight = depth * scale;
        terrainData.heightmapResolution = hmap[0].width + 1;
        terrainData.size = new Vector3(hmap[0].width * scale, currentheight, hmap[0].height * scale);



        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[hmap[0].width, hmap[0].height];

        for (int x = 0; x < hmap[0].width; x++)
        {
            for (int y = 0; y < hmap[0].height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        xCoord = (float)x / hmap[0].width * scale + offsetX;
        yCoord = (float)y / hmap[0].height * scale + offsetY;
        //Debug.Log(Mathf.PerlinNoise(xCoord, yCoord));
        //float newColor = hmap[0].GetPixel(x, y).grayscale;

        //return hmap[0].get = (float)(xCoord);
        return hmap[0].GetPixel(x + (int)xCoord, y + (int)yCoord).grayscale;
        //

        //return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private void SetTerrainSplatMap(Terrain terrain, Texture2D[] textures)
    {
        //var terrainData = terrain.terrainData;

        // The Splat map (Textures)
        TerrainLayer[] splatPrototypes = terrain.terrainData.terrainLayers;
        //splatPrototype = new SplatPrototype[test.Length];
        for (int i = 0; i < textures.Length; i++)
        {
            splatPrototypes[i] = new TerrainLayer();
            splatPrototypes[i].diffuseTexture = textures[i];    //Sets the texture
            splatPrototypes[i].tileSize = new Vector2(100, 100);    //Sets the size of the texture
            splatPrototypes[i].tileOffset = new Vector2(yCoord, xCoord);    //Sets the size of the texture
        }
        terrain.terrainData.terrainLayers = splatPrototypes;
    }


    IEnumerator PlaceObject(float waitTime)
    {
        float one;
        float one2;
        float two;
        float two2;
        float three;
        one = Random.Range(15, terrain.terrainData.size.x * scale);
        one2 = Random.Range(-15, terrain.terrainData.size.x * -1 * scale);

        three = (int)terrain.terrainData.size.z * 2;

        Vector3 randomPos = new Vector3(one, 1, three);
        Vector3 randomPos2 = new Vector3(one2, 1, three);
        two = terrain.SampleHeight(randomPos);
        two2 = terrain.SampleHeight(randomPos);
        Vector3 treePos = new Vector3(one, two, three);
        Vector3 treePos2 = new Vector3(one2, two2, three);

        //Debug.Log(treePos);
        GameObject BabyTree = Instantiate(TreeList[Random.Range(0, TreeList.Length)], treePos, Quaternion.identity, parent.transform);
        GameObject BabyTree2 = Instantiate(TreeList[Random.Range(0, TreeList.Length)], treePos2, Quaternion.identity, parent.transform);
        BabyTree.transform.localScale = new Vector3(threeSize, threeSize, threeSize);
        BabyTree2.transform.localScale = new Vector3(threeSize, threeSize, threeSize);
        treecount++;

        yield return new WaitForSecondsRealtime(waitTime);
    }



}
