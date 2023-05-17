using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InputFieldAnim : MonoBehaviour
{
    [SerializeField] RectTransform panel;
    [SerializeField] TextMeshProUGUI startButton;
    [SerializeField] TextMeshProUGUI restartButton;
    [SerializeField] TextMeshProUGUI textInputPlaceHolder;
    Vector2 open;
    Vector2 closed;
    Color openColor;
    Color closedColor;
    bool state;
    public float speed;
    private void Awake()
    {
        open = new Vector2(panel.sizeDelta.x, 932.99f);
        closed = new Vector2(panel.sizeDelta.x, 40);
        closedColor = new Color(startButton.color.r, startButton.color.g, startButton.color.b, 0);
        openColor = new Color(startButton.color.r, startButton.color.g, startButton.color.b, startButton.color.a);
        panel.sizeDelta = closed;
    }
    private void Update()
    {
        textInputPlaceHolder.faceColor = startButton.faceColor;
        if (state)
        {
            panel.sizeDelta = Vector3.Lerp(panel.sizeDelta, open, speed * Time.deltaTime);
            if (panel.sizeDelta.y > 932.90)
            {
                panel.sizeDelta = open;
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
            if (panel.sizeDelta.y < 39.99f)
            {
                panel.sizeDelta = closed;
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
