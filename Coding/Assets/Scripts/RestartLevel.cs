using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    [HideInInspector] public PlayerBehaviour playerBehaviour;
    TextInput textInput;
    public GameObject playerInGame;
    [SerializeField] GameObject instantiatePlayer;
    [SerializeField] Transform playerRestartPos;
    [SerializeField] GameObject dashPowerInstantiate;
    public GameObject[] dashPowerInGame;
    [SerializeField] Vector3[] dashPowerRestartPos;
    private void Awake()
    {
        textInput = FindObjectOfType<TextInput>();
        dashPowerInGame = GameObject.FindGameObjectsWithTag("Dash");
        dashPowerRestartPos = new Vector3[dashPowerInGame.Length];
        for (int i = 0; i < dashPowerInGame.Length; i++)
        {
            dashPowerRestartPos[i] = dashPowerInGame[i].transform.position;
        }
    }
    public void Restart()
    {
        playerBehaviour.dashCounter = 0;
        textInput.restart = 0;
        Destroy(playerInGame);
        textInput.Stop();
        Instantiate(instantiatePlayer, playerRestartPos.position, Quaternion.identity);
        for (int i = 0; i < dashPowerInGame.Length; i++)
        {
            Destroy(dashPowerInGame[i].gameObject);
            dashPowerInGame[i] = Instantiate(dashPowerInstantiate, dashPowerRestartPos[i], Quaternion.identity);
        }
    }
    public void RestartOnEdit()
    {
        if (textInput.restart >= 1)
        {
            Restart();
            textInput.restart = 0;
        }
    }
    public void DeleteText()
    {
        textInput.inputField.text = null;
    }
}
