using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Linq;
using System;
using System.Windows.Input;

public class Login : MonoBehaviour
{
    public TMP_InputField Name; // Use TMP_InputField
    public TMP_InputField Password; // Use TMP_InputField
    public Button loginButton;
    public Text messageText;
    public GameObject errormsg;
    public DisplayAndFadeUI error;
    public string dbName = "URI=file:msadDB.db";
    void Start()
    {
        // Add listener to the login button
        loginButton.onClick.AddListener(LoginUser);
    }

    void LoginUser()
    {
        string name = Name.text;
        string password = Password.text;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
        {
            error.DisplayTheText(0);
            return;
        }

        bool loginSuccessful = CheckUserCredentials(name, password);

        if (loginSuccessful)
        {
            LoadSaveInfo();
        }
        else
        {
            error.DisplayTheText(1);
        }
    }

    private bool CheckUserCredentials(string name, string password)
    {
        string dbName = "URI=file:msadDB.db";
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Player WHERE Name = @Name AND Password = @Password";
                command.Parameters.Add(new SqliteParameter("@Name", name));
                command.Parameters.Add(new SqliteParameter("@Password", password));

                try
                {
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
                catch (SqliteException ex)
                {
                    ShowMessage("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
    public void LoadSaveInfo()
    {
        string name = Name.text;
        string playerid = GetPlayerIDByName(name);

        if (Name != null)
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT 
                            Player.PlayerID, Player.Name, Player.Password, Player.Gender, Player.Coins, Player.Rating,
                            Status.StudySanity, Status.Entertainment, Status.Mood, Status.Hunger, Status.IsAsleep,
                            Status.SleepToggleTime, Status.StudyToggleTime, Status.MoodToggleTime,
                            Leaderboards.Highscore AS LeaderboardHighscore, Leaderboards.Deaths AS LeaderboardDeaths,
                            FlappyCat.Highscore AS FlappyHighscore, FlappyCat.Deaths AS FlappyDeaths,
                            FoodDrop.Highscore AS FoodHighscore, FoodDrop.Deaths AS FoodDeaths, FoodDrop.TotalMistake AS FoodTotalMistake, FoodDrop.TotalMiss AS FoodTotalMiss,
                            WasteSorting.Highscore AS WasteHighscore, WasteSorting.Deaths AS WasteDeaths, WasteSorting.TotalMistake AS WasteTotalMistake, WasteSorting.TotalMiss AS WasteTotalMiss
                        FROM Player 
                        LEFT JOIN Status ON Player.PlayerID = Status.PlayerID
                        LEFT JOIN TimeManager ON Player.PlayerID = TimeManager.PlayerID
                        LEFT JOIN Leaderboards ON Player.PlayerID = Leaderboards.PlayerID
                        LEFT JOIN FlappyCat ON Player.PlayerID = FlappyCat.PlayerID   
                        LEFT JOIN FoodDrop ON Player.PlayerID = FoodDrop.PlayerID   
                        LEFT JOIN WasteSorting ON Player.PlayerID = WasteSorting.PlayerID   
                        WHERE Player.PlayerID = @PlayerID";
                    command.Parameters.AddWithValue("@PlayerID", playerid);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AAAVP.PlayerID = playerid;
                            AAAVP.Name = reader["Name"].ToString();
                            AAAVP.Password = reader["Password"].ToString();
                            AAAVP.Gender = reader["Gender"].ToString();
                            AAAVP.Coins = Convert.ToInt32(reader["Coins"]);
                            AAAVP.Rating = Convert.ToInt32(reader["Rating"]);
                            AAAVP.StudySanity = Convert.ToSingle(reader["StudySanity"]);
                            AAAVP.Entertainment = Convert.ToSingle(reader["Entertainment"]);
                            AAAVP.Mood = Convert.ToSingle(reader["Mood"]);
                            AAAVP.Hunger = Convert.ToSingle(reader["Hunger"]);
                            AAAVP.IsAsleep = Convert.ToInt32(reader["IsAsleep"]);
                            AAAVP.SleepToggleTime = reader["SleepToggleTime"].ToString();
                            AAAVP.MoodToggleTime = reader["StudyToggleTime"].ToString();
                            AAAVP.LName = reader["Name"].ToString();
                            AAAVP.LRating = Convert.ToInt32(reader["Rating"]);

                            AAAVP.FlappyHighscore = Convert.ToInt32(reader["Highscore"]);
                            AAAVP.FlappyDeaths = Convert.ToInt32(reader["Deaths"]);

                            AAAVP.FoodHighscore = Convert.ToInt32(reader["Highscore"]);
                            AAAVP.FoodDeaths = Convert.ToInt32(reader["Deaths"]);
                            AAAVP.FoodTotalMistake = Convert.ToInt32(reader["TotalMistake"]);
                            AAAVP.FoodTotalMiss = Convert.ToInt32(reader["TotalMiss"]);

                            AAAVP.WasteHighscore = Convert.ToInt32(reader["Highscore"]);
                            AAAVP.WasteDeaths = Convert.ToInt32(reader["Deaths"]);
                            AAAVP.WasteTotalMistake = Convert.ToInt32(reader["TotalMistake"]);
                            AAAVP.WasteTotalMiss = Convert.ToInt32(reader["TotalMiss"]);
                            SceneManager.LoadScene("Rooms");
                        }
                        else
                        {
                            Debug.LogWarning("No save data found for PlayerID: " + playerid);
                        }
                    }
                }
                connection.Close();
            }
        }
        else
        {
            Debug.LogWarning("PlayerID not found or it doesn't have a TextMeshPro component.");
        }
    }
   
    public string GetPlayerIDByName(string name)
    {
        string playerID = null;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT PlayerID FROM Player WHERE Name = @name";
                command.Parameters.AddWithValue("@name", name);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        playerID = reader.GetString(0);
                    }
                }
            }
        }

        return playerID;
    }
    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
        else
        {
            Debug.Log(message);
        }
    }
}