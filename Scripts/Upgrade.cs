using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    UpgradePanel upgradePanel;
    protected Player player;

    [SerializeField] string title, description;

    private void Start()
    {
        upgradePanel = UpgradePanel.Instance;
        player = FindObjectOfType<Player>();
    }

    public abstract void UpgradePlayer();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            upgradePanel.CreatePanel(title, description, this);
    }
}
