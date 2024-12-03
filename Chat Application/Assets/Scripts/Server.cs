using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private List<TcpClient> clients;
    private IPAddress address;

    private int port = 13337;
    public TMP_InputField ipTextField;

    private bool isClientConnected = false;
    private Thread serverThread;

    private NetworkStream stream;
    private byte[] data;

    public static Action<string> onMessageReceived;
    public static Action onConnected;

    public static Server instance;

    public void Awake()
    {
        instance = this;
    }

    public void StartServer(string ip)
    {
        address = IPAddress.Parse(ip);

        server = new TcpListener(address, port);
        server.Start();
        Debug.Log("SERVER :: Start");
        serverThread = new Thread(receiverThread);
        serverThread.Start();
    }

    void receiverThread()
    {
        while (true)
        {
            if (isClientConnected == false)
            {
                client = server.AcceptTcpClient();
                Debug.Log("SERVER :: Client Connected");
                isClientConnected = true;
                stream = client.GetStream();
            }
            else
            {
                //receive the messages
                data = new byte[256];
                string msg = string.Empty;
                int bytes = stream.Read(data, 0, data.Length);
                msg = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Debug.Log("SERVER :: Message received = " + msg);
            }
            Thread.Sleep(100);
        }
    }

    private void OnDisable()
    {
        if (stream != null)
        {
            stream.Close();
        }
        if (server != null)
        {
            server.Stop();
        }
    }
}