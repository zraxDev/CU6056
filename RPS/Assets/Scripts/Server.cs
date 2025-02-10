using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private List<TcpClient> clients;
    private IPAddress address;
    public string ipText;
    private int port = 13337;
    public bool isServerRunning = false;

    private bool isClientConnected = false;
    private Thread serverThread;

    private NetworkStream stream;
    private byte[] data;

    public static Action<string> onMessageReceived;
    public static Action onConnected;

    public static Server instance;
    public UIController uiController;

    public void Awake()
    {
        instance = this;
    }

    public void StartServer(string ip)
    {
        address = IPAddress.Parse(ip);
        ipText = ip;
        Debug.Log("SERVER :: Start");
        serverThread = new Thread(receiverThread);
        serverThread.Start();
        uiController.GameScreen();
    }

    void receiverThread()
    {
        try
        {
            server = new TcpListener(address, port);
            server.Start();
            isServerRunning = true;
            Debug.Log("SERVER :: Started! with IP : " + ipText);
        }
        catch (Exception e)
        {
            Debug.Log("Server Creation Error ::" + e.Message);
        }

        while (isServerRunning)
        {
            if (!isClientConnected)
            {
                client = server.AcceptTcpClient();
                Debug.Log("SERVER :: Client Connected!");
                isClientConnected = true;
                stream = client.GetStream();
                UnityMainThreadDispatcher.Instance().Enqueue(() => onConnected?.Invoke());
            }
            else
            {
                Byte[] bytes = new byte[1024];
                int bytesRead = stream.Read(bytes, 0, bytes.Length);
                if (bytesRead > 0)
                {
                    string msgFromClient = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    //string senderName = userName;
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        onMessageReceived?.Invoke("Client : " + msgFromClient);
                        Debug.Log("Message from Client: " + msgFromClient);
                    });
                }
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
        catch (Exception e)
        {
            Debug.LogError("Error in sending data: " + e.Message);
        }
    }

    public void StopServer()
    {
        if (stream != null)
        {
            stream.Close();
        }
        if (server != null)
        {
            server.Stop();
        }

        serverThread.Abort();
    }

    private void OnDestroy()
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
