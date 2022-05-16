using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks//another photon
{
    public static RoomManager Instance;

    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
        // When enabled, chain the Scene Manager's OnSceneLoaded.
        // Work whenever the scene changes
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // If disabled, clear the scene manager's chain.
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        if (scene.buildIndex == 1)
        // If it's a game scene. 0 is the current start menu scene. 
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity); ;
            //Set the player manager in the photon prefab to that position and that angle
        }
    }
}
