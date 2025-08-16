using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerPresentation : MyNetworkBehaviour
{
    public GameObject firstPersonRig; 
    public GameObject thirdPersonRig;

    public Camera playerCamera;

    // Owner path: base calls this automatically when the object spawns for the local owner
    protected override void OnLocalOwnerStart()
    {
        // Local player sees arms, not body; enable camera & audio
        if (firstPersonRig) firstPersonRig.SetActive(true);
        if (thirdPersonRig) thirdPersonRig.SetActive(false);

        if (playerCamera) playerCamera.enabled = true;
    }

    // Non-owner path: handle what *other clients* should see for this player
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn(); // triggers OnLocalOwnerStart for the owner

        if (!IsOwner)
        {
            // Remotes see the full body; never the 1P arms
            if (firstPersonRig) firstPersonRig.SetActive(false);
            if (thirdPersonRig) thirdPersonRig.SetActive(true);

            // Never run someone else’s camera/audio
            if (playerCamera) playerCamera.enabled = false;
        }
    }
}
