using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System;
using System.Net;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private int port = 13337;
    private NetworkStream stream;
    public string ipText;
    public TMP_InputField msgTextField;
    public static Action<string> onMessageReceived;
    public static Action onConnected;
    public static Client instance;
    private Thread clientThread;

    public void Awake()
    {
        instance = this;
    }

    public void ConnectToServer(string ip)
    {
        Debug.Log("CLIENT :: CONNECTING");
        ipText = ip;
        clientThread = new Thread(new ThreadStart(receiverThread));
        clientThread.Start();
    }

    void receiverThread()
    {
        try
        {
            client = new TcpClient(ipText, port);
            stream = client.GetStream();
            UnityMainThreadDispatcher.Instance().Enqueue(() => onConnected?.Invoke());
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
        while (client.Connected)
        {
            Byte[] bytes = new byte[1024];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);
            if (bytesRead > 0)
            {
                string msgFromServer = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    onMessageReceived?.Invoke("Server : " + msgFromServer);
                    Debug.Log("Message from Server: " + msgFromServer);
                });
            }
            Thread.Sleep(100);
        }
    }

    public void Send(string msg)
    {
        try
        {
            Byte[] bytes = Encoding.ASCII.GetBytes(msg);
            if (client.Connected)
            {
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            Debug.Log("Server Sent: " + msg);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error in sending data: " + exception.Message);
        }
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