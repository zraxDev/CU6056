using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private string serverIP;
    private int port = 1337;
    private NetworkStream stream;
    public GameObject ipTextField;
    public GameObject msgTextField;


    public void ConnectToServer()
    {
        serverIP = ipTextField.GetComponent<InputField>().text;

        Debug.Log("CLIENT :: Connecting to server");
        client = new TcpClient(serverIP, port);
        stream = client.GetStream();
    }

    public void NewMessage()
    {
        string msg = msgTextField.GetComponent<InputField>().text;

        byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }

    private void OnDisable()
    {
        stream.Close();
        client.Close();
    }
}
