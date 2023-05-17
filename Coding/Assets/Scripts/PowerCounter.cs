using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dashCounter;
    [HideInInspector] public PlayerBehaviour player;
    public void Update()
    {
        dashCounter.text = "Dash: " + player.dashCounter;
    }
}
