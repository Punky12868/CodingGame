using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InputFieldAnim : MonoBehaviour
{
    GameObject startButtonGO;
    GameObject restartButtonGO;
    GameObject inputFieldGO;
    [SerializeField] TextMeshProUGUI startButton;
    [SerializeField] TextMeshProUGUI restartButton;
    [SerializeField] RectTransform panel;
    [SerializeField] TextMeshProUGUI textInputPlaceHolder;
    Vector2 open;
    Vector2 closed;
    Vector3 openInputPos;
    Vector3 closeInputPos;
    Vector3 openStartButtonPos;
    Vector3 closeStartButtonPos;
    Vector3 openRestartButtonPos;
    Vector3 closeRestartButtonPos;
    Color openColor;
    Color closedColor;
    bool state;
    [SerializeField] float yButtonPos;
    [SerializeField] float yInputPos;
    [SerializeField] float speed;
    [SerializeField] float speedSecond;
    private void Awake()
    {
        startButtonGO = GameObject.FindGameObjectWithTag("StartB");
        restartButtonGO = GameObject.FindGameObjectWithTag("RestartB");
        inputFieldGO = GameObject.FindGameObjectWithTag("InputField");

        open = new Vector2(panel.sizeDelta.x, 932.99f);
        closed = new Vector2(panel.sizeDelta.x, 40);

        openInputPos = new Vector3(inputFieldGO.transform.position.x, inputFieldGO.transform.position.y);
        closeInputPos = new Vector3(inputFieldGO.transform.position.x, inputFieldGO.transform.position.y - yInputPos);

        openStartButtonPos = new Vector3(startButtonGO.transform.position.x, startButtonGO.transform.position.y);
        closeStartButtonPos = new Vector3(startButtonGO.transform.position.x, startButtonGO.transform.position.y - yButtonPos);

        openRestartButtonPos = new Vector3(restartButtonGO.transform.position.x, restartButtonGO.transform.position.y);
        closeRestartButtonPos = new Vector3(restartButtonGO.transform.position.x, restartButtonGO.transform.position.y - yButtonPos);

        openColor = new Color(startButton.color.r, startButton.color.g, startButton.color.b, startButton.color.a);
        closedColor = new Color(startButton.color.r, startButton.color.g, startButton.color.b, 0);

        panel.sizeDelta = closed;
        inputFieldGO.transform.position = closeInputPos;
        startButtonGO.transform.position = closeStartButtonPos;
        restartButtonGO.transform.position = closeRestartButtonPos;
    }
    private void Update()
    {
        textInputPlaceHolder.faceColor = startButton.faceColor;
        if (state)
        {
            panel.sizeDelta = Vector3.Lerp(panel.sizeDelta, open, speed * Time.deltaTime);
            inputFieldGO.transform.position = Vector3.Lerp(inputFieldGO.transform.position, openInputPos, speedSecond * Time.deltaTime);
            startButtonGO.transform.position = Vector3.Lerp(startButtonGO.transform.position, openStartButtonPos, speedSecond * Time.deltaTime);
            restartButtonGO.transform.position = Vector3.Lerp(restartButtonGO.transform.position, openRestartButtonPos, speedSecond * Time.deltaTime);
            if (panel.sizeDelta.y > 932.985)
            {
                panel.sizeDelta = open;
                inputFieldGO.transform.position = openInputPos;
                startButtonGO.transform.position = openStartButtonPos;
                restartButtonGO.transform.position = openRestartButtonPos;
            }
            else if (panel.sizeDelta.y > 897)
            {
                startButton.faceColor = Color.Lerp(startButton.faceColor, openColor, speed * Time.deltaTime);
                restartButton.faceColor = Color.Lerp(startButton.faceColor, openColor, speed * Time.deltaTime);
                if (startButton.faceColor.a > 254)
                {
                    startButton.faceColor = openColor;
                    restartButton.faceColor = openColor;
                }
            }
        }
        else
        {
            panel.sizeDelta = Vector3.Lerp(panel.sizeDelta, closed, speed * Time.deltaTime);
            inputFieldGO.transform.position = Vector3.Lerp(inputFieldGO.transform.position, closeInputPos, speedSecond * Time.deltaTime);
            startButtonGO.transform.position = Vector3.Lerp(startButtonGO.transform.position, closeStartButtonPos, speedSecond * Time.deltaTime);
            restartButtonGO.transform.position = Vector3.Lerp(restartButtonGO.transform.position, closeRestartButtonPos, speedSecond * Time.deltaTime);
            if (panel.sizeDelta.y < 39.99f)
            {
                panel.sizeDelta = closed;
                inputFieldGO.transform.position = closeInputPos;
                startButtonGO.transform.position = closeStartButtonPos;
                restartButtonGO.transform.position = closeRestartButtonPos;
            }
            else if (panel.sizeDelta.y < 899.05)
            {
                startButton.faceColor = Color.Lerp(startButton.faceColor, closedColor, speed * Time.deltaTime);
                restartButton.faceColor = Color.Lerp(startButton.faceColor, closedColor, speed * Time.deltaTime);
                if (startButton.faceColor.a < 0.1)
                {
                    startButton.faceColor = closedColor;
                    restartButton.faceColor = closedColor;
                }
            }
        }
    }
    public void StartAnim()
    {
        if (!state)
        {
            state = true;
        }
        else
        {
            state = false;
        }
    }
}
