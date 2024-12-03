using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject clientMenu;
    public GameObject serverMenu;
    public GameObject messageScreen;

    private void Awake()
    {
        Server.onConnected += MessageScreen;
        Client.onConnected += MessageScreen;

        Server.onMessageReceived += showMessage;
        Client.onMessageReceived += showMessage;
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        clientMenu.SetActive(false);
        serverMenu.SetActive(false);
        messageScreen.SetActive(false);
}

    public void ClientMenu()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(true);
        serverMenu.SetActive(false);
        messageScreen.SetActive(false);
    }

    public void ServerMenu()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(false);
        serverMenu.SetActive(true);
        messageScreen.SetActive(false);
    }

    public void MessageScreen()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(false);
        serverMenu.SetActive(false);
        messageScreen.SetActive(true);
    }

    private void showMessage(string msg)
    {
        Screen.ShowMessage(msg);
    }
}
