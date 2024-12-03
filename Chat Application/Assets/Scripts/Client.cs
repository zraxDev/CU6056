using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private int port = 13337;
    private NetworkStream stream;
    public TMP_InputField ipTextField;
    public TMP_InputField msgTextField;
    public static Action<string> onMessageReceived;
    public static Action onConnected;
    public static Client instance;

    public void Awake()
    {
        instance = this;
    }

    public void ConnectToServer(string ip)
    {
        Debug.Log("CLIENT :: Connecting to server");
        client = new TcpClient(ip, port);
        stream = client.GetStream();
    }

    public void NewMessage()
    {
        string msg = msgTextField.text;

        byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }

    private void OnDisable()
    {
        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Dispose();
        }
    }
}