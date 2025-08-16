using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;   // <-- needed for Button
using TMPro;

public class ConnectUIScript : MonoBehaviour
{
    public GameObject cameraTemp;

    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Start()
    {
        hostButton.onClick.AddListener(HostButtonOnClick);
        clientButton.onClick.AddListener(ClientButtonOnClick);
    }

    private void HostButtonOnClick()
    {
        cameraTemp.active = false;
        NetworkManager.Singleton.StartHost();
    }

    private void ClientButtonOnClick()
    {
        cameraTemp.active = false;

        NetworkManager.Singleton.StartClient();
    }
}