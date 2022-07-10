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

    /// <summary> Этот метод открывает подключение к БД. </summary>
    private static void OpenConnection()
    {
        dbconnection = new SqliteConnection("URI = file:" + path);
        cmd = new SqliteCommand(dbconnection);
        dbconnection.Open();
    }

    /// <summary> Этот метод закрывает подключение к БД. </summary>
    public static void CloseConnection()
    {
        dbconnection.Close();
        cmd.Dispose();
    }

    /// <summary> Этот метод выполняет запрос query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        CloseConnection();
    }

    /// <summary> Этот метод выполняет запрос query и возвращает ответ запроса. </summary>
    /// <param name="query"> Собственно запрос. </param>
    /// <returns> Возвращает значение 1 строки 1 столбца, если оно имеется. </returns>
    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        cmd.CommandText = query;
        var answer = cmd.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    /// <summary> Этот метод возвращает таблицу, которая является результатом выборки запроса query. </summary>
    /// <param name="query"> Собственно запрос. </param>
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
