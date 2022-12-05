using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemplate : MonoBehaviour
{
    public TMPro.TMP_Text nameTxt;
    public string id;

    public void DisplayUI(string name,string id)
    {
        nameTxt.text = name;
        this.id = id;
    }
}
