using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageView : MonoBehaviour
{
    public TextMeshProUGUI Message;
    public void ShowMessage(string msg)
    {
        Message.text = msg;
    }
}
