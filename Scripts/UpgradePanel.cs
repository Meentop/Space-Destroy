using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public static UpgradePanel Instance;

    [SerializeField] List<Upgrade> upgrades;
    Upgrade activeUpgrade = null;

    [SerializeField] GameObject panel;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Shake applyText;
    [SerializeField] GameObject portal;
    [SerializeField] Transform portalSpawnPoint;

    Player player;
    SoundEffector sound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        sound = SoundEffector.Instance;
    }

    [SerializeField] float riseShakeDistance;

    private void Update()
    {
        if (activeUpgrade != null)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                HidePanel();
                player.OnMovement();
                applyText.distance = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
                applyText.Begin();
            if (Input.GetKey(KeyCode.Space))
                applyText.distance += riseShakeDistance;

            if (applyText._timer >= 2f)
            {
                activeUpgrade.UpgradePlayer();
                HidePanel();
                HideUpgrades();
                sound.PlayUpgrade();
                player.OnMovement();
            }
        }
    }

    public void ShowUpgades()
    {
        foreach (Upgrade upgrade in upgrades)
            upgrade.gameObject.SetActive(true);
        HidePanel();
    }

    [SerializeField] int gameScene = 1;

    void HideUpgrades()
    {
        foreach (Upgrade upgrade in upgrades)
            upgrade.gameObject.SetActive(false);
        SpawnPortal(gameScene);
    }

    public void CreatePanel(string title, string description, Upgrade upgrade)
    {
        player.OffMovement();
        this.title.text = title.ToUpper();
        this.description.text = description.ToUpper();
        activeUpgrade = upgrade;
        panel.SetActive(true);
    }

    void HidePanel()
    {
        activeUpgrade = null;
        panel.SetActive(false);
    }


    public void SpawnPortal(int loadScene)
    {
        portal.GetComponent<Portal>().loadScene = loadScene;
        Instantiate(portal, portalSpawnPoint.position, Quaternion.identity);
    }
}
