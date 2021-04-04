using DG.Tweening;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lblDisplayName, profileScene, addQuestionScene;
    [SerializeField] private InputField tbxDisplayName, tbxAge, tbxQuestion, tbxAnswer;
    public string popUpMessage;
    private Auth auth;
    private User user;
    private Questions questions;
    public User User => user;

    void Awake()
    {
        auth = GameObject.Find("Auth").GetComponent<Auth>();
        //if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
        //{
        //    DontDestroyOnLoad(transform.gameObject);
        //}     
    }
    public void GetUser ()
    {
        Clear();
        Database.Instance.GetReference("users/" + auth.User.UserId).GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)
            {
                Debug.Log("HATA");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                user = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
            }
        });
       
        Debug.Log("GET USER ÇALIŞTI");       
    }
    private void Start()
    {
        DOTween.Init();
        GetUser();
    }

    private void Update()
    {
       lblDisplayName.GetComponent<Text>().text = user.displayName;
       FillTbx();
    }
    public void Logout()
    {
        auth.Logout();
    }

    public void OpenProfile()
    {
        GetUser();
        profileScene.transform.DOMoveY(0, 1.5f).SetEase(Ease.OutBounce);
    }
    public void CloseProfile()
    {
        Clear();
        profileScene.transform.DOMoveY(9, 1.5f);
    }

    public void OpenAddQestion()
    {
        addQuestionScene.transform.DOMoveX(0, 1.5f).SetEase(Ease.OutBounce);
    }
    public void CloseAddQestion()
    {
        Clear();
        addQuestionScene.transform.DOMoveX(-12.6f, 1.5f);
    }
    public void EditProfile()
    {
        string displayName = tbxDisplayName.text == "" ? user.displayName : tbxDisplayName.text;
        int age = tbxAge.text == "" ? user.userAge : Convert.ToInt32(tbxAge.text);

        Debug.Log("isim: " + displayName);
        Debug.Log("yaş: " + age);

        User userS = new User(displayName, age);
        string json = JsonUtility.ToJson(userS);

        Debug.Log("değer: " + json);

        Database.Instance.GetReference("users/" + auth.User.UserId).SetRawJsonValueAsync(json);

        GetUser();
    }
    public void DeleteProfile()
    {
        auth.Delete();
    }
    private void Clear()
    {
        tbxDisplayName.text = "";
        tbxAge.text = "";
        tbxQuestion.text = "";
        tbxAnswer.text = "";
    }
    private void FillTbx()
    {
         tbxDisplayName.transform.Find("Placeholder").GetComponent<Text>().text = user.displayName;
         tbxAge.transform.Find("Placeholder").GetComponent<Text>().text = user.userAge.ToString();
    }

    public void AddQuestion()
    {
        if (tbxQuestion.text.Length>=10 && (tbxAnswer.text.Length>=4 && tbxAnswer.text.Length<=10))
        {
            Debug.Log("Soru: " + tbxQuestion.text);
            Debug.Log("Cevap: " + tbxAnswer.text);

            Questions questionS = new Questions(tbxQuestion.text, tbxAnswer.text.ToUpper());
            string json = JsonUtility.ToJson(questionS);

            Debug.Log("değer: " + json);
            CloseAddQestion();
            Database.Instance.RootReference.Child("questions").Push().SetRawJsonValueAsync(json);
        }
        else
        {
            MessagePopUp pop = gameObject.GetComponent<MessagePopUp>();
            pop.PopUp(popUpMessage);
        }

      //  GetUser();
    }

    public void StartGame()
    {
        if (auth)
        {           
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("Login olunmamış");
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EndGame()
    {
        lblDisplayName.GetComponent<Text>().text = user.displayName;
    }
}
