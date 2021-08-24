using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hightmapTerrain : MonoBehaviour
{

    public Texture2D[] hmap;
    public int depth = 20;
    public float scale = 0;

    public GameObject Tree;
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


    void Start()
    {
        SetTerrainSplatMap(terrain, test);
    }

    // Start is called before the first frame update
    void Update()
    {


        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        if (heyaa) { 
            SetTerrainSplatMap(terrain, test);
        }
        //SetTerrainSplatMap

        if (treecount <= 35) {

            treecount = 35;
        }
        if (treecount >= 35)
        {
            StartCoroutine(PlaceObject());
            offsetY += Time.deltaTime * animeSpeed;
            parent.transform.Translate(-Time.deltaTime * animeSpeed, 0,0);
          
        }
      


    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = hmap[0].width + 1;
        terrainData.size = new Vector3(hmap[0].width, depth, hmap[0].height);
      
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
        float xCoord = (float)x / hmap[0].width * scale + offsetX;
        float yCoord = (float)y / hmap[0].height * scale + offsetY;
        //Debug.Log(Mathf.PerlinNoise(xCoord, yCoord));
        //float newColor = hmap[0].GetPixel(x, y).grayscale;

        //return hmap[0].get = (float)(xCoord);
        return hmap[0].GetPixel(x+(int)xCoord, y + (int)yCoord).grayscale;
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
            splatPrototypes[i].tileOffset = new Vector2((int)offsetY, offsetX);    //Sets the size of the texture
        }
        terrain.terrainData.terrainLayers = splatPrototypes;
    }


    IEnumerator PlaceObject()
    {
        int one;
        float two;
        int three;
            one = Random.Range(5, hmap[0].width);
           
            three = Random.Range(5, hmap[0].height);
            two = terrain.terrainData.GetHeight(one, three);
            Vector3 treePos = new Vector3(one, two, three );



        GameObject BabyTree = Instantiate(Tree, treePos, Quaternion.identity,parent.transform);
        treecount++;

        yield return new WaitForSeconds(2f);
    }
    // Update is called once per frame

}
