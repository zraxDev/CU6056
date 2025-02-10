using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject clientMenu;
    public GameObject serverMenu;
    public GameObject waitScreen;
    public GameObject gameScreen;

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        clientMenu.SetActive(false);
        serverMenu.SetActive(false);
        waitScreen.SetActive(false);
        gameScreen.SetActive(false);
    }
    public void ServerMenu()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(false);
        serverMenu.SetActive(true);
        waitScreen.SetActive(false);
        gameScreen.SetActive(false);
    }

    public void ClientMenu()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(true);
        serverMenu.SetActive(false);
        waitScreen.SetActive(false);
        gameScreen.SetActive(false);
    }

    public void WaitScreen()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(false);
        serverMenu.SetActive(false);
        waitScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void GameScreen()
    {
        mainMenu.SetActive(false);
        clientMenu.SetActive(false);
        serverMenu.SetActive(false);
        waitScreen.SetActive(false);
        gameScreen.SetActive(true);
    }
}
