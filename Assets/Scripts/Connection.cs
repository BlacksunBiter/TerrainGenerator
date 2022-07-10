using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.IO;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    public InputField fieldW;
    public InputField fieldH;
    public InputField fieldSO;
    public InputField fieldSP;
    public InputField fieldS;
    public InputField fieldO;
    public InputField fieldP;
    public InputField fieldL;

    public InputField fieldSiDS;
    public InputField fieldSeDS;
    public Dropdown dropRDS;
    public InputField fieldADS;
    public InputField fieldMaDS;
    public InputField fieldMiDS;
    public GameObject Ndiamond;

    public Dropdown dropPerID;

    private string path = "";
    public SqliteConnection dbconnection;
    SqliteCommand cmd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setConnection()
    {
        path = Application.dataPath + "/StreamingAssets/db.bytes";
        dbconnection = new SqliteConnection("URI = file:" + path);
        cmd = new SqliteCommand(dbconnection);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
            //cmd.CommandText = "INSERT INTO Models (Titel, Description, Location) VALUES('Rock-1', 'Low polystone stone, in fact it is one stone', 'Rock1.fbx'); ";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Models (Titel, Description, Location) VALUES('Rock-2', 'Low polystone stone, in fact it is one stone', 'Rock2.fbx'); ";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Models (Titel, Description, Location) VALUES('Rock-3', 'Low polystone stone, in fact it is one stone', 'Rock3.fbx'); ";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Models (Titel, Description, Location) VALUES('Rock-4', 'Low polystone stone, in fact it is one stone', 'Rock4.fbx'); ";
            //cmd.ExecuteNonQuery();

            ////cmd.CommandText = "INSERT INTO Perlin (Width, Height, SeedOctaveShift, SeedPermutionTable, Scale, Octaves, Persistence, Lacunarity, StartX, StartY, PathScreen) VALUES('1024', '1024', '0', '0', '100', '3', '0.5', '0',); );
            //cmd.CommandText = "INSERT INTO Perline (Width, Height) VALUES('1024', '1024'); ";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO 'Diamond-Square' (DegreeOfTwo) VALUES('10'); ";
            //cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM Models";
            SqliteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                Debug.Log(String.Format("{0} {1} ", r[0], r[1]));
            }
            dbconnection.Close();
        }
        else
            Debug.Log("Error");
        Debug.Log("end");

    }

    public void setInsertPerline()
    {
        int width = int.Parse(fieldW.text);
        int height = int.Parse(fieldH.text);
        int seedOctaveShift = int.Parse(fieldSO.text);
        int seedPermutationTable = int.Parse(fieldSP.text);
        float scale = float.Parse(fieldS.text);
        int octaves = int.Parse(fieldO.text);
        float persistence = float.Parse(fieldP.text);
        float lacunarity = float.Parse(fieldL.text);

        string a = "INSERT INTO Perlin (Width, Height, SeedOctaveShift, SeedPermutionTable, Scale, Octaves, Persistence, Lacunarity, StartX, StartY, PathScreen) VALUES('" + width + "', '" + height + "', '" + seedOctaveShift + "', '" + seedPermutationTable + "', '" + scale + "', '" + octaves + "', '" + persistence + "', '" + lacunarity + "','0','0','/');";
        MyDataBase.ExecuteQueryWithoutAnswer(a);
    }

    public void setDownPerline()
    {
        int index = dropPerID.value;
        int iD = int.Parse(dropPerID.options[index].text);

        string a = "SELECT Width, Height, SeedOctaveShift, SeedPermutionTable, Scale, Octaves, Persistence, Lacunarity, PathScreen FROM Perlin WHERE Perlin.ID = '" + iD + "'";
        List<string> items = MyDataBase.GetString(a);

        fieldW.text = items[0];
        fieldH.text = items[1];
        fieldSO.text = items[2];
        fieldSP.text = items[3];
        fieldS.text = items[4];
        fieldO.text = items[5];
        fieldP.text = items[6];
        fieldL.text = items[7];
    }
}
