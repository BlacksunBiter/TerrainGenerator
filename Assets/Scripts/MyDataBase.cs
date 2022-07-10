using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.IO;


public class MyDataBase : MonoBehaviour
{
    private static string path = "";
    public static SqliteConnection dbconnection;
    static SqliteCommand cmd;

    static MyDataBase()
    {
        path = Application.dataPath + "/StreamingAssets/db.bytes";
    }

    /// <summary> ���� ����� ��������� ����������� � ��. </summary>
    private static void OpenConnection()
    {
        dbconnection = new SqliteConnection("URI = file:" + path);
        cmd = new SqliteCommand(dbconnection);
        dbconnection.Open();
    }

    /// <summary> ���� ����� ��������� ����������� � ��. </summary>
    public static void CloseConnection()
    {
        dbconnection.Close();
        cmd.Dispose();
    }

    /// <summary> ���� ����� ��������� ������ query. </summary>
    /// <param name="query"> ���������� ������. </param>
    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        CloseConnection();
    }

    /// <summary> ���� ����� ��������� ������ query � ���������� ����� �������. </summary>
    /// <param name="query"> ���������� ������. </param>
    /// <returns> ���������� �������� 1 ������ 1 �������, ���� ��� �������. </returns>
    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        cmd.CommandText = query;
        var answer = cmd.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    /// <summary> ���� ����� ���������� �������, ������� �������� ����������� ������� ������� query. </summary>
    /// <param name="query"> ���������� ������. </param>
    public static DataTable GetTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, dbconnection);

        DataSet DS = new DataSet();
        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();

        return DS.Tables[0];
    }
    public static List<string> GetString(string query)
    {
        OpenConnection();
        List<string> items = new List<string>();
        cmd.CommandText = query;
        SqliteDataReader r = cmd.ExecuteReader();
        while (r.Read())
        {
            items.Add(String.Format("{0} {1} ", r[0], r[1]));
        }
        CloseConnection();
        return items;
    }
}
