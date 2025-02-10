using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitScreen : MonoBehaviour
{
    public GameObject waitScreen;
    public TMP_Text waitText;
    private string[] textTable = { "WAITING FOR CLIENT." , "WAITING FOR CLIENT..", "WAITING FOR CLIENT..." };

    void OnEnable()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            for (int i = 0; i < textTable.Length; i++)
            {
                waitText.text = textTable[i];
                yield return new WaitForSeconds(1f);
            }
        }
    }
}