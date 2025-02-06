using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    public TextMeshProUGUI message;
    public void ShowMessage(string msg)
    {
        message.text = msg;
    }
}
