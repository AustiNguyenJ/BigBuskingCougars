using System;
using UnityEngine;

public class PlayerTeleportManager : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Transform spawnPoint;

    void OnEnable()
    {
        GlobalEventAsset.Instance.StartListening<OnSceneGroupLoadedEvent>(OnNewSceneGroupLoaded);
    }

    void OnDisable()
    {
        GlobalEventAsset.Instance.StopListening<OnSceneGroupLoadedEvent>(OnNewSceneGroupLoaded);
    }

    void OnNewSceneGroupLoaded(OnSceneGroupLoadedEvent data) => TeleportPlayerToSpawnPoint();

    void TeleportPlayerToSpawnPoint()
    {
        GameObject spawnPointObj = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (Validate.AnyNull(spawnPointObj, playerTransform)) return;
        spawnPoint = spawnPointObj.transform;
        playerTransform.position = spawnPoint.position;
    }
}
