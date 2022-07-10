using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NoisesLibrary;
using UnityEngine.IO;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;

public class GenerateNoiseApply : MonoBehaviour
{
    static float[] noiseMap;
    static float[,] noiseMap2;

    public InputField fieldW;
    public InputField fieldH;
    public InputField fieldSO;
    public InputField fieldSP;
    public InputField fieldS;
    public InputField fieldO;
    public InputField fieldP;
    public InputField fieldL;
    public InputField fieldHT;

    public InputField fieldSiDS;
    public InputField fieldSeDS;
    //public Dropdown dropRDS;
    public InputField fieldADS;
    public InputField fieldMaDS;
    public InputField fieldMiDS;
    public InputField fieldHTDS;
    public GameObject Ndiamond;

    public Dropdown dropPerID;
    public Dropdown dropDaimID;

    public Image image;
    public Image preweiv;
    public Terrain terrain;

    Texture2D texture;
    Color[] colorMap;
    public Material material;

    public void SetPerlineTerrain()
    {
        if (Ndiamond.gameObject.activeSelf == false)
        {
            int width = int.Parse(fieldW.text);
            int height = int.Parse(fieldH.text);
            int heightT = int.Parse(fieldHT.text);



            float[,] heights = new float[width, height];

            material.SetTexture("_HeightTex", texture);

            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    heights[i, k] = texture.GetPixel(i, k).grayscale;
                }
            }

            // Применяем изменения
            terrain.terrainData.size = new Vector3(width, heightT, height);
            terrain.terrainData.heightmapResolution = width;
            terrain.terrainData.SetHeights(0, 0, heights);
        }
        else
        {
            int size = int.Parse(fieldSiDS.text);
            int seed = int.Parse(fieldSeDS.text);


            //int index = dropRDS.value;
            //bool rect = bool.Parse(dropRDS.options[index].text);

            float Rmin = float.Parse(fieldMiDS.text);
            float Rmax = float.Parse(fieldMaDS.text);
            int heightT = int.Parse(fieldHTDS.text);


            int width;
            int height;
            //if (rect)
            //{
            //    width = (int)Mathf.Pow(2, size) * 2;
            //    height = (int)Mathf.Pow(2, size);
            //}
            //else
            //{
            width = (int)Mathf.Pow(2, size);
            height = (int)Mathf.Pow(2, size);
            //}

            float[,] heights = new float[width, height];

            material.SetTexture("_HeightTex", texture);

            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    heights[i, k] = texture.GetPixel(i, k).grayscale;
                }
            }

            // Применяем изменения
            terrain.terrainData.size = new Vector3(width, heightT, height);
            terrain.terrainData.heightmapResolution = width;
            terrain.terrainData.SetHeights(0, 0, heights);
        }

    }

    public void SetPerlineNoises()
    {
        if (Ndiamond.gameObject.activeSelf == false)
        {
            int width = int.Parse(fieldW.text);
            int height = int.Parse(fieldH.text);
            int seedOctaveShift = int.Parse(fieldSO.text);
            int seedPermutationTable = int.Parse(fieldSP.text);
            float scale = float.Parse(fieldS.text);
            int octaves = int.Parse(fieldO.text);
            float persistence = float.Parse(fieldP.text);
            float lacunarity = float.Parse(fieldL.text);



            NoisesClass ns = new NoisesClass();
            noiseMap = ns.PerlineNoise(width, height, seedOctaveShift, seedPermutationTable, scale, octaves, persistence, lacunarity, 0, 0);

            colorMap = new Color[noiseMap.Length];
            for (int i = 0; i < noiseMap.Length; i++)
            {
                colorMap[i] = Color.Lerp(Color.black, Color.white, noiseMap[i]);
            }

            ApplyColorMap(width, height, colorMap);
        }
        else
        {
            UInt16 size = UInt16.Parse(fieldSiDS.text);
            UInt16 seed = UInt16.Parse(fieldSeDS.text);
            float asp = float.Parse(fieldADS.text);



            //int index = dropRDS.value;
            //bool rect = bool.Parse(dropRDS.options[index].text);


            float Rmin = float.Parse(fieldMiDS.text);
            float Rmax = float.Parse(fieldMaDS.text);


            int width;
            int height;
            //if (rect)
            //{
            //    width = (int)Mathf.Pow(2, size) * 2;
            //    height = (int)Mathf.Pow(2, size);
            //}
            //else
            //{
            width = (int)Mathf.Pow(2, size);
            height = (int)Mathf.Pow(2, size);
            //}

            NoisesClass ns = new NoisesClass();

            noiseMap2 = ns.DiamondSquareNoise(size, seed, false, asp, Rmin, Rmax);
            colorMap = new Color[width * height];
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    colorMap[j * height + i] = Color.Lerp(Color.black, Color.white, noiseMap2[i, j]);
                }

            ApplyColorMap(width, height, colorMap);
        }
    }

    private void ApplyColorMap(int width, int height, UnityEngine.Color[] colors)
    {
        texture = new Texture2D(width, height);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        texture.SetPixels(colors);
        texture.Apply();

        Sprite tempSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = tempSprite;
    }


    public void setInsertPerline()
    {
        SetPerlineNoises();
        byte[] bytes = texture.EncodeToPNG();
        string path;
        string a;
        if (Ndiamond.gameObject.activeSelf == false)
        {
            int width = int.Parse(fieldW.text);
            int height = int.Parse(fieldH.text);
            int seedOctaveShift = int.Parse(fieldSO.text);
            int seedPermutationTable = int.Parse(fieldSP.text);
            float scale = float.Parse(fieldS.text);
            int octaves = int.Parse(fieldO.text);
            float persistence = float.Parse(fieldP.text);
            float lacunarity = float.Parse(fieldL.text);

            path = "/ScreenNoise/Perline" + int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT seq FROM sqlite_sequence WHERE sqlite_sequence.name=Perline")) + 1 + ".png";

            ////UnityEngine.Windows.File.WriteAllBytes(Application.dataPath + path, bytes);
            

            a = "INSERT INTO Perline (Width, Height, SeedOctaveShift, SeedPermutionTable, Scale, Octaves, Persistence, Lacunarity, StartX, StartY, PathScreen) VALUES('" + width + "', '" + height + "', '" + seedOctaveShift + "', '" + seedPermutationTable + "', '" + scale + "', '" + octaves + "', '" + persistence + "', '" + lacunarity + "','0','0','" + path + "');";

        }
        else
        {
            int size = int.Parse(fieldSiDS.text);
            int seed = int.Parse(fieldSeDS.text);

            //int index = dropRDS.value;
            //string rect = dropRDS.options[index].text;

            float asp = float.Parse(fieldADS.text);
            float Rmin = float.Parse(fieldMiDS.text);
            float Rmax = float.Parse(fieldMaDS.text);

            path = "/ScreenNoise/Diamond-Square" + int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT seq FROM sqlite_sequence WHERE sqlite_sequence.name='Diamond-Square'")) + 1 + ".png";

            ////UnityEngine.Windows.File.WriteAllBytes(Application.dataPath + path, bytes);

            a = "INSERT INTO 'Diamond-Square' (DegreeOfTwo, Seed, Rectangle, Asperity, RMin, RMax, PathScreen) VALUES('" + size + "', '" + seed + "', 'false', '" + asp + "', '" + Rmin + "', '" + Rmax + "','" + path + "');";
        }
        MyDataBase.ExecuteQueryWithoutAnswer(a);
    }

    public void setDownPerline()
    {
        DataTable table;
        string a;
        List<object> test = new List<object>();
        if (Ndiamond.gameObject.activeSelf == false)
        {
            int index = dropPerID.value;
            int iD = int.Parse(dropPerID.options[index].text);

            a = "SELECT Width, Height, SeedOctaveShift, SeedPermutionTable, Scale, Octaves, Persistence, Lacunarity FROM Perline WHERE Perline.ID = '" + iD + "'";
            table = MyDataBase.GetTable(a);
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    test.Add(row[column]);
                }
                fieldW.text = test[0].ToString();
                fieldH.text = test[1].ToString();
                fieldSO.text = test[2].ToString();
                fieldSP.text = test[3].ToString();
                fieldS.text = test[4].ToString();
                fieldO.text = test[5].ToString();
                fieldP.text = test[6].ToString();
                fieldL.text = test[7].ToString();
            }
        }
        else
        {
            int index = dropDaimID.value;
            int iD = int.Parse(dropDaimID.options[index].text);

            a = "SELECT DegreeOfTwo, Seed, Rectangle, Asperity,	RMin, RMax FROM Diamond-Square WHERE Diamond-Square.ID = '" + iD + "'";
            table = MyDataBase.GetTable(a);
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    test.Add(row[column]);
                }
                fieldSiDS.text = test[0].ToString();
                fieldSeDS.text = test[1].ToString();
                fieldADS.text = test[3].ToString();
                fieldMiDS.text = test[4].ToString();
                fieldMaDS.text = test[5].ToString();
            }
        }
    }

    public void setSaveImageMapRaw()
    {
        //byte[] bytes = texture.EncodeToPNG();
        //UnityEngine.Windows.File.WriteAllBytes("E://SavedScreen.png", bytes);
        //bytes = texture.EncodeToJPG();
        //UnityEngine.Windows.File.WriteAllBytes("E://SavedScreen.jpg", bytes);
        var map = texture.GetRawTextureData();
        
        ////UnityEngine.Windows.File.WriteAllBytes(Application.dataPath + "/" + Mathf.Floor(UnityEngine.Random.value * 10000) + ".RAW", map);

        //byte[] bytes = UnityEngine.Windows.File.ReadAllBytes("E://SavedScreen.RAW");
        //texture.LoadRawTextureData(bytes);
        //texture.Apply();
    }
    public void setSaveImageMapPng()
    {
        byte[] bytes = texture.EncodeToPNG();
        ////UnityEngine.Windows.File.WriteAllBytes(Application.dataPath + "/" + Mathf.Floor(UnityEngine.Random.value * 10000) + ".png", bytes);
    }

    public void setPreveiwPerline()
    {
        int index = dropPerID.value;
        int iD = int.Parse(dropPerID.options[index].text);
        string a = "SELECT PathScreen FROM Perline WHERE Perline.ID = '" + iD + "'";
        string im = MyDataBase.ExecuteQueryWithAnswer(a);
        ////byte[] bytes = UnityEngine.Windows.File.ReadAllBytes(Application.dataPath + im);
        Texture2D texture = new Texture2D(1024, 1024);
        texture.filterMode = FilterMode.Trilinear;
        ////texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1024, 1024), new Vector2(0.5f, 0.0f), 100.0f);

        preweiv.sprite = sprite;

    }
    public void setPreveiwDaimond()
    {

    }
}
