using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public TMP_InputField ipInputfield;
    public static bool isServer = false;

    // Start is called before the first frame update
    void HostServer()
    {
        Server.instance.StartServer(ipInputfield.text);
        isServer = true;
    }

    // Update is called once per frame
    void JoinServer()
    {
        Client.instance.ConnectToServer(ipInputfield.text);
        isServer = false;
    }
}
