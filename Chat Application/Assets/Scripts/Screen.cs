using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public MessageView messageView;
    public TMP_InputField input;
    public Transform parent;

    public void ShowMessage(string message)
    {
        MessageView view = Instantiate(messageView, parent);
        view.ShowMessage(message);
    }

    public void Send()
    {
        if (Menu.isServer)
        {
            Server.instance.Send(input.text);
            ShowMessage("Server : " + input.text);
        }
        else
        {
            Client.instance.Send(input.text);
            ShowMessage("Client : " + input.text);
        }
    }
}