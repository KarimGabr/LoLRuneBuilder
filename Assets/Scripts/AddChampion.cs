using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class AddChampion : MonoBehaviour
{

    private string connection, query;

    IDbConnection dbconnection;
    IDbCommand dbcmd;

    public void insertValue(string championName)
    {
        //Add path to database
        connection = "URI=file:" + Application.dataPath + "/Plugins/Champions.s3db";

        using (dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            dbcmd = dbconnection.CreateCommand();
            query = string.Format("insert into ChampionsRunes (name, isPlayed) values (\"{0}\", \"{1}\")", championName, 1);
            dbcmd.CommandText = query;
            dbcmd.ExecuteScalar();
            dbconnection.Close();
        }

        Vector3 iconPosition = new Vector3(transform.parent.position.x + 400f, transform.parent.position.y, transform.parent.position.z);

        GameObject championIcon = Instantiate(Resources.Load("champion-icon"), iconPosition, Quaternion.identity) as GameObject;
        championIcon.transform.SetParent(transform.parent);

        string spritePath = "Images/Champions Icons/" + "Anivia";
        championIcon.transform.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(spritePath);
    }
}
