using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public TMP_InputField serverIPInputfield;
    public TMP_InputField clientIPInputfield;
    public static bool isServer = false;

    public void HostServer()
    {
        Server.instance.StartServer(serverIPInputfield.text);
        isServer = true;
    }

    public void JoinServer()
    {
        Client.instance.ConnectToServer(clientIPInputfield.text);
        isServer = false;
    }
}
