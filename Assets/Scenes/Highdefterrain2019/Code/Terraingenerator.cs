using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Terraingenerator : MonoBehaviour
{

    public int depth = 20;
    public int witdh = 512;
    public int height = 512;
    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float movementSpeed = 5f;
    public BitArray[] byteArray;

    public string filename = "/Advanced/terrain.raw";



    // Start is called before the first frame update
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        //offsetX += Time.deltaTime * movementSpeed;
    }


    TerrainData GenerateTerrain( TerrainData terrainData)
    {
        terrainData.heightmapResolution = witdh + 1;
        terrainData.size = new Vector3(witdh, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }


    float[,] GenerateHeights()
    {
        //Texture2D tex = new Texture2D(2, 2);
        //tex.LoadImage(imageAsset.bytes);

        string aFileName = Application.dataPath + filename;
        //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
        //BinaryReader br = new BinaryReader(fs);
        //br.BaseStream.Seek(0, SeekOrigin.Begin);
        float[,] heights = new float[witdh, height];
        using (var file = System.IO.File.OpenRead(aFileName))
        using (var reader = new System.IO.BinaryReader(file))
            for (int x = 0; x < witdh; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = (float)reader.ReadInt16();
            }
        }
        return heights;
    }

    //float CalculateHeight(int x, int y)
    //{
    //    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
    //    BinaryReader br = new BinaryReader(fs);

    //    float v = (float)reader.ReadUInt16();
    //    float xCoord = (float)x / witdh * scale + offsetX;
    //    float yCoord = (float)y / height * scale + offsetY;

    //    return Mathf.PerlinNoise(xCoord, yCoord);
    //}


    //    void LoadTerrain(string filename, TerrainData terrainData)
    //    {
    //        float[,] dat = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
    //        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
    //        BinaryReader br = new BinaryReader(fs);
    //        br.BaseStream.Seek(0, SeekOrigin.Begin);
    //        for (int i = 0; i < terrainData.heightmapWidth; i++)
    //        {
    //            for (int j = 0; j < terrainData.heightmapHeight; j++)
    //            {
    //                dat[i, j] = (float)br.ReadSingle();
    //            }
    //        }
    //        br.Close();
    //        terrainData.SetHeights(0, 0, dat);
    //        heights = terrainData.GetHeights(50, 50, 100, 100);
    //    }
}
