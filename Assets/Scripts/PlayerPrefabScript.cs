using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrefabScript : MonoBehaviour
{
    public GameObject MainCameraPrefab;
    public GameObject PlayerFollowCameraPrefab;

    /// <summary>
    /// Must be called for players who are not the current player
    /// </summary>
    public void DestroyControllers()
    {
        Destroy(GetComponent<ThirdPersonController>());
        Destroy(GetComponent<PlayerInput>());
    }

    public void InitCameras()
    {
        GameObject mainCamera = Instantiate(MainCameraPrefab);

        GetComponent<ThirdPersonController>().PlayerCamera = mainCamera;

        GameObject playerFollowCamera = Instantiate(PlayerFollowCameraPrefab);

        playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform.Find("PlayerCameraRoot");
    }

    void Update()
    {

    }
}
