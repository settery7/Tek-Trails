using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Registration : MonoBehaviour
{
    public TMP_InputField Name; // Use TMP_InputField
    public TMP_InputField Password; // Use TMP_InputField
    public Button maleButton;
    public Button femaleButton;
    public Button registerButton;
    public Text messageText;
    public GameObject errormsg;
    public DisplayAndFadeUI error;
    private string forguid;
    public string dbName = "URI=file:msadDB.db";
    private string gender;

    public static string GenerateUniqueId()
    {
        return System.Guid.NewGuid().ToString();
    }

    void Start()
    {
        CreateDB();
        // Add listeners to the buttons
        maleButton.onClick.AddListener(() => SetGender("Male"));
        femaleButton.onClick.AddListener(() => SetGender("Female"));
        registerButton.onClick.AddListener(Register);
    }

    void SetGender(string selectedGender)
    {
        gender = selectedGender;
        Debug.Log("Selected Gender: " + gender);
    }

    public void Register()
    {
        string name = Name.text;

        if (string.IsNullOrWhiteSpace(Name.text) || string.IsNullOrWhiteSpace(Password.text))
        {
            error.DisplayTheText(0);
            return;
        }
        else if (IsNameTaken(name))
        {
            error.DisplayTheText(1); // Assuming 1 is the error code for a name already taken
            return;
        }
        AddSaveInfo();
        SceneManager.LoadScene("MainMenu");
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Player (
                    PlayerID VARCHAR(36) PRIMARY KEY, 
                    Name TEXT, 
                    Password TEXT,
                    Gender TEXT,
                    Coins INTEGER,
                    Rating INTEGER
                );";
                command.ExecuteNonQuery();
            }
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Status (
                    PlayerID VARCHAR(36), 
                    StudySanity FLOAT, 
                    Entertainment FLOAT, 
                    Mood FLOAT, 
                    Hunger FLOAT,
                    PRIMARY KEY(PlayerID),
                    FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID)
                );";
                command.ExecuteNonQuery();
            }
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS TimeManager (
                    PlayerID VARCHAR(36), 
                    IsAsleep INTEGER,
                    SleepToggleTime TEXT,
                    StudyToggleTime TEXT,
                    MoodToggleTime TEXT,
                    PRIMARY KEY(PlayerID),
                    FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID)
                );";
                command.ExecuteNonQuery();
            }
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Leaderboards (
                    PlayerID VARCHAR(36),
                    Name TEXT, 
                    Rating INTEGER,
                    PRIMARY KEY(PlayerID),
                    FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID)
                );";
                command.ExecuteNonQuery();
            }
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS FlappyCat (
                    PlayerID VARCHAR(36),
                    Highscore INTEGER, 
                    Deaths INTEGER,
                    PRIMARY KEY(PlayerID),
                    FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID)
                );";
                command.ExecuteNonQuery();
            }
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS FoodDrop (
                    PlayerID VARCHAR(36),
                    Highscore INTEGER, 
                    Deaths INTEGER,
                    TotalMistake INTEGER,
                    TotalMiss INTEGER,
                    PRIMARY KEY(PlayerID),
                    FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID)
                );";
                command.ExecuteNonQuery();
            }
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS WasteSorting (
                    PlayerID VARCHAR(36),
                    Highscore INTEGER, 
                    Deaths INTEGER,
                    TotalMistake INTEGER,
                    PRIMARY KEY(PlayerID),
                    FOREIGN KEY(PlayerID) REFERENCES Player(PlayerID)
                );";
                command.ExecuteNonQuery();
            }
        }
    }

    public void AddSaveInfo()
    {
        forguid = GenerateUniqueId();
        string name = Name.text;
        string password = Password.text;

        if (string.IsNullOrWhiteSpace(Name.text) || string.IsNullOrWhiteSpace(Password.text))
        {
            error.DisplayTheText(0);
            return;
        }
        else if (IsNameTaken(name))
        {
            error.DisplayTheText(1); // Assuming 1 is the error code for a name already taken
            return;
        }
        else
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                INSERT INTO Player (PlayerID, Name, Password, Gender, Coins, Rating) 
                VALUES (@playerid, @username, @password, @gender, @coins, @rating)";

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@username", name));
                    command.Parameters.Add(new SqliteParameter("@password", password));
                    command.Parameters.Add(new SqliteParameter("@gender", gender));
                    command.Parameters.Add(new SqliteParameter("@coins", 500));
                    command.Parameters.Add(new SqliteParameter("@rating", 0));
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                INSERT INTO Status (PlayerID, StudySanity, Entertainment, Mood, Hunger) 
                VALUES (@playerid, @studysanity, @entertainment, @mood, @hunger)";

                    command.Parameters.Clear();  // Clear previous parameters

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@studysanity", 100));
                    command.Parameters.Add(new SqliteParameter("@entertainment", 100));
                    command.Parameters.Add(new SqliteParameter("@mood", 100));
                    command.Parameters.Add(new SqliteParameter("@hunger", 100));
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                INSERT INTO TimeManager (PlayerID, IsAsleep, SleepToggleTime, StudyToggleTime, MoodToggleTime) 
                VALUES (@playerid, @IsAsleep, @SleepToggleTime, @StudyToggleTime, @MoodToggleTime)";

                    command.Parameters.Clear();  // Clear previous parameters

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@IsAsleep", 1));
                    command.Parameters.Add(new SqliteParameter("@SleepToggleTime", DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("@StudyToggleTime", DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("@MoodToggleTime", DBNull.Value));
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                INSERT INTO Leaderboards (PlayerID, Name, Rating) 
                VALUES (@playerid, @Name, @Rating)";

                    command.Parameters.Clear();  // Clear previous parameters

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@Name", name));
                    command.Parameters.Add(new SqliteParameter("@Rating", 0));
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                INSERT INTO FlappyCat (PlayerID, Highscore, Deaths) 
                VALUES (@playerid, @Highscore, @Deaths)";

                    command.Parameters.Clear();  // Clear previous parameters

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@Highscore", 0));
                    command.Parameters.Add(new SqliteParameter("@Deaths", 0));
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                INSERT INTO FoodDrop (PlayerID, Highscore, Deaths, TotalMistake, TotalMiss) 
                VALUES (@playerid, @Highscore, @Deaths, @TotalMistake, @TotalMiss)";

                    command.Parameters.Clear();  // Clear previous parameters

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@Highscore", 0));
                    command.Parameters.Add(new SqliteParameter("@Deaths", 0));
                    command.Parameters.Add(new SqliteParameter("@TotalMistake", 0));
                    command.Parameters.Add(new SqliteParameter("@TotalMiss", 0));
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                INSERT INTO WasteSorting (PlayerID, Highscore, Deaths, TotalMistake) 
                VALUES (@playerid, @Highscore, @Deaths, @TotalMistake)";

                    command.Parameters.Clear();  // Clear previous parameters

                    command.Parameters.Add(new SqliteParameter("@playerid", forguid));
                    command.Parameters.Add(new SqliteParameter("@Highscore", 0));
                    command.Parameters.Add(new SqliteParameter("@Deaths", 0));
                    command.Parameters.Add(new SqliteParameter("@TotalMistake", 0));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
    private bool IsNameTaken(string name)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Player WHERE Name = @name";
                command.Parameters.Add(new SqliteParameter("@name", name));

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
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
