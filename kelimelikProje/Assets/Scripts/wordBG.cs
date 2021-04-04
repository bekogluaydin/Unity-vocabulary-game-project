using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wordBG : MonoBehaviour
{
    public TMP_Text txtWord;
    public Animator animationWord;

    public void Active()
    {
        txtWord.gameObject.SetActive(true);
        //animationWord.speed = 1f;
    }

    public void DeActive()
    {
        txtWord.gameObject.SetActive(false);
        //animationWord.speed = 0f;
    }

    public void SetText (string text)
    {
        txtWord.text = text;
    }
}
