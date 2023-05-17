using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    RestartLevel restartLevel;
    public PlayerBehaviour playerScript;
    public TMP_InputField inputField;
    public Button startButton;
    [HideInInspector] public int restart;
    void Awake()
    {
        restartLevel = FindObjectOfType<RestartLevel>();
        startButton.onClick.AddListener(StartExecution);
        //inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    void StartExecution()
    {
        if (restart == 0)
        {
            string[] commands = inputField.text.Split('\n');

            StartCoroutine(ExecuteCommands(commands));
            restart++;
        }
        else
        {
            restart++;
            restartLevel.RestartOnEdit();

            string[] commands = inputField.text.Split('\n');

            StartCoroutine(ExecuteCommands(commands));
        }
    }

    /*void OnInputFieldValueChanged(string value)
    {
        restartLevel.RestartOnEdit();
    }*/
    public void Stop()
    {
        StopAllCoroutines();
    }
    IEnumerator ExecuteCommands(string[] commands)
    {
        foreach (string command in commands)
        {
            playerScript.Test(command);

            yield return new WaitForSeconds(playerScript.waitTime);
        }
    }
}
