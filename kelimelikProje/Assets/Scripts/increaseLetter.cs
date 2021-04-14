using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Database;

public class increaseLetter : MonoBehaviour
{
    private Auth auth;
    private User user;
    private CountdownTimer timer;
    private SecondCountdownTimer secondTimer;
    private Questions questions1;

    public List<Questions> QuestionsList;
    [HideInInspector]
    public Questions CurrentQuestion;
    [SerializeField] private TMP_Text txtQuestion, txtQuestionPoint, txtQuestionNumber, txtUserPoint, txtDisplayName, txtPlayTime, txtRemainTime;
    public TMP_InputField tbxGuess;
    public Button btnGetLetter, btnGuess, btnAnswer;
    public string popUpMessage;

    [SerializeField] private GameObject EndGameScene, IncLetterObject, SecondTimerObject;

    //private GameManager gameManager;
    
    void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        auth = GameObject.Find("Auth").GetComponent<Auth>();
        // user = gameManager.User;
        GetUser();
        gQestion(getAnswer);
        EndGameScene.SetActive(false);
    }
    void Start()
    {            
        timer = gameObject.GetComponent<CountdownTimer>();
        secondTimer = gameObject.GetComponent<SecondCountdownTimer>();
        GetQuestion();
    }
    public void Logout()
    {
        auth.Logout();
    }

    public void GetUser()
    {
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

    public void gQestion(int getAnswer)
    {
        Database.Instance.GetReference("questions").GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted)
            {
                Debug.Log("HATA");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, object> questions = snapshot.Value as Dictionary<string, object>;
                foreach (var item in questions)
                {
                    string questionId = item.Key;
                    if (JsonUtility.FromJson<Questions>(snapshot.Child(questionId).GetRawJsonValue()).answer.Length == getAnswer)
                    {
                        questions1 = JsonUtility.FromJson<Questions>(snapshot.Child(questionId).GetRawJsonValue());
                        QuestionsList.Add(questions1);
                    }
                    else Debug.Log("İstenilen levele uymuyor soru.");
                }
                Debug.Log("soru list: " + QuestionsList.Count);



                //for (int i = 0; i < snapshot.ChildrenCount; i++)
                //{
                //    if (JsonUtility.FromJson<Questions>(snapshot.Child(i.ToString()).GetRawJsonValue()).answer.Length == getAnswer)
                //    {
                //        Debug.Log("a: " + snapshot.Child(i.ToString()).GetRawJsonValue());
                //        questions1 = JsonUtility.FromJson<Questions>(snapshot.Child(i.ToString()).GetRawJsonValue());
                //        QuestionsList.Add(questions1);
                //    }
                //    else Debug.Log("İstenilen levele uymuyor soru.");
                //}
                //questions1 = JsonUtility.FromJson<Questions>(snapshot.Child("2").GetRawJsonValue());
                //Debug.Log("cevap: " + questions1.answer);
            }
        });
    }
   
    public void EndGame()
    {
        timer.StopTimer();
        SecondTimerObject.SetActive(false);
        BtnAnswerDisable();
        BtnGetLetterDisable();
        txtQuestionPoint.text = "0";
        txtDisplayName.text = user.displayName;
        txtUserPoint.text = UserPoint.ToString();
        txtRemainTime.text = timer.txtTimer.text;
        EndGameScene.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    int RandomQuestion, UserPoint, QuestionNumber;
    void GetQuestion()
    {
        QuestionNumber++;

        Debug.Log("Puan: " + UserPoint);
        txtQuestion.text = "Soru Yok!";
        foreach (Transform obje in this.transform)
        {
            Destroy(obje.gameObject);
        }

        if (QuestionsList.Count >0)
        {
            txtQuestionNumber.text = ("Soru Numarası " + QuestionNumber+ "/14" );
            timer.StartTimer();
            BtnGetLetterEnable();
            BtnAnswerEnable();        

            RandomQuestion = Random.Range(0, QuestionsList.Count);
            CurrentQuestion.question = QuestionsList[RandomQuestion].question;
            CurrentQuestion.answer = QuestionsList[RandomQuestion].answer;

            for (int i = 0; i < CurrentQuestion.answer.Length; i++)
            {
                GameObject Inc = Instantiate(IncLetterObject, transform);
                var inc = Inc.GetComponent<wordBG>();
                inc.SetText(CurrentQuestion.answer[i].ToString());
                QuestionsList[RandomQuestion].Acilmayanlar.Add(inc.txtWord.GetComponent<TMP_Text>());
                txtQuestion.text = CurrentQuestion.question;
                inc.DeActive();
            }
            CurrentQuestion.Acilmayanlar = QuestionsList[RandomQuestion].Acilmayanlar;
            GetQestionPoint();
        }
        else
        {
            EndGame();
        }
    }
    public void EndQuestion()
    {
        CloseSecondTimer();
        correctAnswer++;
        QuestionsList.RemoveAt(RandomQuestion);
        foreach (TMP_Text letter in CurrentQuestion.Acilmayanlar)
        {
            letter.gameObject.SetActive(true);
        }
        Invoke("GetQuestion", 1.7f);

        if (correctAnswer % 2 == 0)
        {
            QuestionsList.Clear();
            getAnswer++;
            gQestion(getAnswer);
        }
        TbxGuessClear();
        tbxGuess.interactable = false;
        btnGuess.interactable = false;
    }
    
    int correctAnswer = 0, getAnswer = 4;
    public void GuessTheAnswer()
    {
        if (tbxGuess.text.ToUpper() == CurrentQuestion.answer)
        {
            Debug.Log("bildiniz, yeni soru geliyor!");
            UserPoint += CurrentQuestion.Acilmayanlar.Count * 100;
            EndQuestion();      
        }
        else
        {
            MessagePopUp pop = gameObject.GetComponent<MessagePopUp>();
            pop.PopUp(popUpMessage);
            TbxGuessClear();
        }
    }
    public void GuessTheAnswerIsActive()
    {
        timer.StopTimer();
        GetSecondTimer();
        BtnAnswerDisable();
        BtnGetLetterDisable();
        tbxGuess.interactable = true;
        btnGuess.interactable = true;
    }
    public void ReqLetter()
    {
        BtnGetLetterDisable();

        if (CurrentQuestion.Acilmayanlar.Count > 1)
        {
            int randomLetter = Random.Range(0, CurrentQuestion.Acilmayanlar.Count);
            CurrentQuestion.Acilmayanlar[randomLetter].gameObject.SetActive(true);
            CurrentQuestion.Acilmayanlar.RemoveAt(randomLetter);

            if (CurrentQuestion.Acilmayanlar.Count <= 1)
            {
                MessagePopUp pop = gameObject.GetComponent<MessagePopUp>();
                pop.PopUp("Harf Alma Hakkınız Kalmadı!");

                Debug.Log("Harf Alma Hakkınız Kalmadı!");
                BtnGetLetterDisable();
            }
            else
            {            
                Invoke("BtnGetLetterEnable", 1.4f);
            }
            GetQestionPoint();
        }
    }
    public void BtnAnswerEnable()
    {
        btnAnswer.interactable = true;
    }
    public void BtnAnswerDisable()
    {
        btnAnswer.interactable = false;
    }
    public void BtnGetLetterDisable()
    {
        btnGetLetter.interactable = false;
    }
    public void BtnGetLetterEnable()
    {
        btnGetLetter.interactable = true;
    }
    public void GetQestionPoint()
    {
        txtQuestionPoint.text = (CurrentQuestion.Acilmayanlar.Count * 100).ToString();
    }
    public void GetSecondTimer()
    {
        SecondTimerObject.SetActive(true);
        secondTimer.StartTimer();
    }
    public void CloseSecondTimer()
    {
        secondTimer.StopTimer();
        SecondTimerObject.SetActive(false);
    }
    public void TbxGuessClear()
    {
        tbxGuess.text = "";
    }
}

[System.Serializable]
public class Questions
{
    public string question;
    public string answer;
    public List<TMP_Text> Acilmayanlar;

    public Questions(string question, string answer)
    {
        this.question = question;
        this.answer = answer;
    }
    public Questions(string question, string answer, List<TMP_Text> acilmayanlar)
    {
        this.question = question;
        this.answer = answer;
        Acilmayanlar = acilmayanlar;
    }
}
