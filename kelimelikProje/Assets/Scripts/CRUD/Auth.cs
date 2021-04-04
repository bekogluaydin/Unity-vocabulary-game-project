using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    public FirebaseUser User => user;
    [SerializeField] private GameObject tbxEmail, tbxPassword;
    
    private void Awake()
    {
        InitializeFirebase();
        if (GameObject.FindGameObjectsWithTag("Auth").Length == 1)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                SceneManager.LoadScene("Login");
            }
            user = auth.CurrentUser;
       
            if (signedIn)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public void Register()
    {
        string email = tbxEmail.GetComponent<Text>().text;
        string password = tbxPassword.GetComponent<Text>().text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            string userID = newUser.UserId;
            string displayName = newUser.DisplayName == "" ? userID : newUser.DisplayName;
            int age = 999;

            User user = new User(displayName, age);
            string json = JsonUtility.ToJson(user);

            Debug.Log(json);
            Database.Instance.GetReference("users/"+userID).SetRawJsonValueAsync(json);
        });
        }

    
    public void Login()
    {
        string email = tbxEmail.GetComponent<Text>().text;
        string password = tbxPassword.GetComponent<Text>().text;
        int userVarmi = 0;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            string userID = newUser.UserId;
            
            DataSnapshot snapshot = Database.Instance.GetReference("users/").GetValueAsync().Result;
            Dictionary<string, object> users = snapshot.Value as Dictionary<string, object>;
            foreach (var item in users)
            {
                string userId = item.Key;
                Debug.Log("userId: " + userId);

                if (userId == userID)
                {
                    userVarmi++;
                }
            }
            if (userVarmi == 0)
            {
                string displayName = newUser.DisplayName == "" ? userID : newUser.DisplayName;
                int age = 999;

                User user = new User(displayName, age);
                string json = JsonUtility.ToJson(user);

                Debug.Log(json);
                Database.Instance.GetReference("users/" + userID).SetRawJsonValueAsync(json);
            }          
        });
    }

    public void Delete()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Database.Instance.GetReference("users/" + auth.CurrentUser.UserId).RemoveValueAsync();
            user.DeleteAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }
                //Database.Instance.GetReference("users/" + auth.CurrentUser.UserId).RemoveValueAsync();
                //Debug.Log(Database.Instance.GetReference("users/" + user.UserId));
                Debug.Log("User deleted successfully.");              
            });
        }
    }
    public void Logout()
    {
        auth.SignOut();
    }
}
