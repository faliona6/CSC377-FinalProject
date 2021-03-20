using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRManager : MonoBehaviour
{
    public bool showVR = false;
    public float playerScale = 5f;
    private Transform player;

    private void Start()
    {
        player = GameLoopManager.Instance.player.transform;
    }

    private void Update()
    {
        UnityEngine.XR.XRSettings.showDeviceView = true;
        player.localScale = new Vector3(playerScale, playerScale, playerScale);
    }
}

