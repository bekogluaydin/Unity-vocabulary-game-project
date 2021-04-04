using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private int minutes; 
    [SerializeField]
    private int seconds;

    private int m, s;

    [SerializeField]
    public TMP_Text txtTimer;

    private increaseLetter increaseLetter;

    void Start()
    {
        increaseLetter = gameObject.GetComponent<increaseLetter>();
        m = minutes;
        s = seconds;
    }

    public void StartTimer()
    {      
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
                Debug.Log("EndGame Girdi");
                increaseLetter.EndGame();
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
        if(s<10)
        {
            txtTimer.text = m.ToString() + ":0" + s.ToString();
        }
        else
        {
            txtTimer.text = m.ToString() + ":" + s.ToString();
        }
    }
}
