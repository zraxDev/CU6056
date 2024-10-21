using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private List<TcpClient> clients;

    private int port = 1337;
    private IPAddress serverIp = IPAddress.Parse("127.0.0.1");
    public GameObject ipTextField;

    private bool isClientConnected = false;
    private Thread serverThread;

    private NetworkStream stream;
    private byte[] data;

    public void StartServer()
    {
        serverIp = IPAddress.Parse(ipTextField.GetComponent<InputField>().text);

        server = new TcpListener(serverIp, port);
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
        stream.Close();
        server.Stop();
    }
}