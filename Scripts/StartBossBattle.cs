using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    [SerializeField] Transform portalSpawnPoint;

    [SerializeField] GameObject portal;

    public static StartBossBattle Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPortal(int loadScene)
    {
        portal.GetComponent<Portal>().loadScene = loadScene;
        Instantiate(portal, portalSpawnPoint.position, Quaternion.identity);
    }
}
