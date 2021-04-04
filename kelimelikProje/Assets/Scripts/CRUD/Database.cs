using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public static class Database
{
    public static FirebaseDatabase Instance { get; set; }
     
    static Database()
    {
        Instance = FirebaseDatabase.DefaultInstance;
    }
}
