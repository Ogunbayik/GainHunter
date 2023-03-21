using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera Instance { get; private set; }

    [SerializeField] private GameObject virtualCamera;
    [SerializeField] private GameObject battleCamera;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        VirtualCameraActivate();
    }

    public void VirtualCameraActivate()
    {
        virtualCamera.SetActive(true);
        battleCamera.SetActive(false);
    }

    public void BattleCameraActivate()
    {
        battleCamera.SetActive(true);
        virtualCamera.SetActive(false);
    }

    public GameObject GetBattleCamera()
    {
        return battleCamera;
    }


}
