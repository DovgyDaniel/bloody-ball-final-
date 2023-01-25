using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class Registration : MonoBehaviour
{

    public InputField usernameField;
    public InputField passwordField;
    public Button submitButton;
    private string connectionStr;
    public Text userExists;



    public void verifyInputLengths()
    {
        submitButton.interactable = (usernameField.text.Length >= 4 && usernameField.text.Length <= 20 && passwordField.text.Length >= 5 && passwordField.text.Length <= 20);
        usernameField.text = usernameField.text.Replace(" ", string.Empty);
        passwordField.text = passwordField.text.Replace(" ", string.Empty);
    }

    //проверить, существует ли имя пользователя
    public bool usernameNotExists()
    {
        userExists.gameObject.SetActive(false); //отключить пользовательский интерфейс перед повторной проверкой, существует ли пользователь.
        IDbCommand dbcmd;
        IDataReader reader;
        IDbConnection dbcon = new SqliteConnection(connectionStr);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string q_selectFromTable = "SELECT username FROM players WHERE username='" + usernameField.text + "';";

        dbcmd.CommandText = q_selectFromTable;
        reader = dbcmd.ExecuteReader();

        if (reader.Read() == false)// имя пользователя прошло проверку
        { 
            return true;
        }

        return false;
    }

    public void insertUser()
    {
        if (usernameNotExists())
        {
            //Заполняет массив байтов криптостойкой последовательностью случайных значений.
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]); //генерировать последовательность данных
            var pbkdf2 = new Rfc2898DeriveBytes(passwordField.text, salt, 10000);
            byte[] hashBytes = pbkdf2.GetBytes(20);

            string savedHash = Convert.ToBase64String(hashBytes);
            string savedSalt = Convert.ToBase64String(salt);

            IDbCommand dbcmd;
            IDbConnection dbcon = new SqliteConnection(connectionStr);
            dbcon.Open();
            dbcmd = dbcon.CreateCommand();
            string insertToPlayers = "INSERT INTO players (username,hash,salt) VALUES ('"
                + usernameField.text + "','" + savedHash + "','" + savedSalt + "');";
            dbcmd.CommandText = insertToPlayers;
            dbcmd.ExecuteNonQuery();// добавление аккаунта
            dbcon.Close(); //закрытое соединение с базой данных
            SceneManager.LoadScene("loginMenu"); //если пользователь был создан, войдите в систему перед игрой.
        }
        else
        {
            userExists.gameObject.SetActive(true);//включить пользовательский интерфейс, который предупреждает, что имя пользователя занято
        }
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu77");
    }

    void Start()
    {
        //connectionStr = "URI=file:" + Application.persistentDataPath + "/StreamingAssets/000.db";
        //connectionStr = "URI=file:" + Application.platform + "/Users\\DanielPye\\Desktop/база.db";
        connectionStr = "URI=file:" + Application.dataPath + "/StreamingAssets/DataUsers.db";
    }


    
   
}
