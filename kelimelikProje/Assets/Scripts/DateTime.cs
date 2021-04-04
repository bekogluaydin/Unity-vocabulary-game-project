using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateTime : MonoBehaviour
{
    public TMP_Text txtDateTime;
    void Start()
    {
        string time = System.DateTime.Now.ToString();
        txtDateTime.text = time;
    }
}
