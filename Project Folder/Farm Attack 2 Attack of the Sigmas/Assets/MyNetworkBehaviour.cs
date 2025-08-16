using Unity.Netcode;
using UnityEngine;

public class MyNetworkBehaviour : NetworkBehaviour
{
    protected bool IsLocalOwner => IsOwner && IsClient;

    protected virtual void OnLocalOwnerStart() { }
    protected virtual void OnLocalOwnerUpdate() { }

    public override void OnNetworkSpawn()
    {
        if (IsLocalOwner)
        {
            OnLocalOwnerStart();
        }
    }

    void Update()
    {
        if (IsLocalOwner)
        {
            OnLocalOwnerUpdate();
        }
    }
}
