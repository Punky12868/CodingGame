using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPlayer : MonoBehaviour
{
    CinemachineVirtualCamera cCamera;
    public GameObject player;
    private void Awake()
    {
        cCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        cCamera.Follow = player.transform;
    }
}
