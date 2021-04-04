using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondCountdownTimer : MonoBehaviour
{
    [SerializeField]
    private int minutes;
    [SerializeField]
    private int seconds;

    private int m, s;

    [SerializeField]
    public TMP_Text txtSecondTimer;

    private increaseLetter increaseLetter;

    void Start()
    {
        increaseLetter = gameObject.GetComponent<increaseLetter>();
    }

    public void StartTimer()
    {
        m = minutes;
        s = seconds;
        writerTimer(m, s);
        Invoke("UpdateTimer", 1f);
    }
    void UpdateTimer()
    {
        s--;
        if (s < 0)
        {
            if (m == 0)
            {
                Debug.Log("EndQuestion Girdi");
                increaseLetter.EndQuestion();
                return;
            }
            else
            {
                m--;
                s = 59;
            }
        }
        writerTimer(m, s);
        Invoke("UpdateTimer", 1f);
    }
    public void StopTimer()
    {
        CancelInvoke();
    }

    private void writerTimer(int m, int s)
    {
        if (s < 10)
        {
            txtSecondTimer.color = Color.red;
            txtSecondTimer.text = m.ToString() + ":0" + s.ToString();
        }
        else
        {
            txtSecondTimer.color = Color.white;
            txtSecondTimer.text = m.ToString() + ":" + s.ToString();
        }
    }
}
