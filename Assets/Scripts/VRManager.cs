using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRManager : MonoBehaviour
{
    public bool showVR = false;
    public float playerScale = 5f;
    public Transform player;

    private void Update()
    {
        // TODO: Move somewhere else

        UnityEngine.XR.XRSettings.showDeviceView = true;
        player.localScale = new Vector3(playerScale, playerScale, playerScale);

    }
}

