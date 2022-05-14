using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

public class GameNetworkManager : MonoBehaviour
{
    public static string ipAddress = "127.0.0.1";
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 400));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            ipAddress = GUILayout.TextField(ipAddress);
            StartButtons();
        }
        else
        {
            StatusLabels();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        NetworkManager.Singleton.gameObject.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();

        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();

        if (GUILayout.Button("Client"))
        {
            NetworkManager.Singleton.gameObject.GetComponent<UNetTransport>().ConnectAddress = ipAddress;
            NetworkManager.Singleton.StartClient();
        }
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
        GUILayout.Label("Connected to ip : " + NetworkManager.Singleton.gameObject.GetComponent<UNetTransport>().ConnectAddress);
    }
}
