using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class db : MonoBehaviour
{
    private string connection, query;

    IDbConnection dbconnection;
    IDbCommand dbcmd;

	void Start ()
    {
        //Add path to database
        connection = "URI=file:" + Application.dataPath + "/Plugins/Champions.s3db";

        //test function insertValue()
        //insertValue("Amumu", 0);

        //test function updateValue()
        //insertValue("Amumu", 0);

        //test function deleteValue()
        //insertValue("Amumu", 0);

        //Read Values
        readers();
	}
	
    private void readers()
    {
        using (dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            dbcmd = dbconnection.CreateCommand();

            query = "SELECT * " + "FROM ChampionsRunes";
            dbcmd.CommandText = query;
            IDataReader reader = dbcmd.ExecuteReader();

            float x = 100f;
            float y = 0f;

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int isPlayed = reader.GetInt32(2);

 
                if (isPlayed == 1)
                {
                    if (transform.childCount > 1) x += 70f;
                    if (transform.childCount % 8 == 0)
                    {
                        y -= 60f;
                        x = 30f;
                    }
                        
                    Debug.Log("transform name = " + transform.name + " transform xpos =  " + transform.position.x + " transform ypos = " + transform.position.y);
                    Vector3 iconPosition = new Vector3(transform.position.x + x, transform.position.y - 25f + y, transform.position.z);
                    Debug.Log("transform name = " + transform.name + " transform xpos =  " + transform.position.x + " transform ypos = " + transform.position.y);
                    GameObject championIcon = Instantiate(Resources.Load("champion-icon"), iconPosition, Quaternion.identity) as GameObject;
                    championIcon.transform.SetParent(transform);

                    string spritePath = "Images/Champions Icons/" + name;
                    championIcon.transform.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(spritePath);
                    championIcon.transform.localScale = new Vector3(1f,1f,1f);
                }

                Debug.Log("value = " + id + " " + " champion = " + name + " " + "isPlayed = " + isPlayed);
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconnection.Close();
            dbconnection = null;
        }
    }

    private void insertValue(string championName, int isPlayed)
    {
        using (dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            dbcmd = dbconnection.CreateCommand();
            query = string.Format("insert into ChampionsRunes (name, isPlayed) values (\"{0}\", \"{1}\")",championName, isPlayed);
            dbcmd.CommandText = query;
            dbcmd.ExecuteScalar();
            dbconnection.Close();
        }
    }

    private void updateValue()
    {
        using (dbconnection = new SqliteConnection(connection))
        {

            dbconnection.Open();
            dbcmd = dbconnection.CreateCommand();
       //   query = string.Format("UPDATE Usersinfo set Name=\"{0}\", Email=\"{1}\", Address=\"{2}\" WHERE ID=\"{3}\" ", name, email, address, id);// table name
            dbcmd.CommandText = query;
            dbcmd.ExecuteScalar();
            dbconnection.Close();
        }
    }

    private void deleteValue()
    {
        using (dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            dbcmd = dbconnection.CreateCommand();
     //     query = string.Format("Delete from Usersinfo WHERE ID=\"{0}\"", id);// table name
            dbcmd.CommandText = query;
            dbcmd.ExecuteScalar();
            dbconnection.Close();
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
