using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;

public class CollectibleManager : MonoBehaviour
{
    // public static CollectibleManager instance;

    public List<AbilityCollectible> collectibles = new List<AbilityCollectible>();

    public GameObject CollectiblesPanel;
    public GameObject CollectiblesList;
    Transform CollectiblesListTrans;
    public GameObject CollectibleSlotPrefab;

    public Button CollectiblesButton;
    public GameObject ButtonNotif;


    public AbilityCollectible boots;
    public AbilityCollectible gloves;
    public AbilityCollectible wingedBoots;


    // Start is called before the first frame update
    void Start() {
        // instance = this;

        if (CollectiblesList != null) {
            CollectiblesListTrans = CollectiblesList.transform;
        }

        CollectiblesButton.onClick.AddListener(delegate { ToggleInventory(); });
    }

    public void AddItem(AbilityCollectible collectible) {
        if (!collectibles.Contains(collectible)) {
            collectibles.Add(collectible);
        }

        if (CollectiblesPanel.activeSelf) {
            ListItems();
        } else {
            ButtonNotif.SetActive(true);
        }
    }

    public void ListItems() {
        // collectibles list
        foreach (Transform child in CollectiblesListTrans) {
            Destroy(child.gameObject);
        }

        foreach (AbilityCollectible collectible in collectibles) {
            GameObject obj = Instantiate(CollectibleSlotPrefab, CollectiblesListTrans);
            
            Image collectibleIcon = obj.transform.Find("CollectibleButton/Icon").GetComponent<Image>();
            collectibleIcon.sprite = collectible.sprite;

            TextMeshProUGUI collectibleName = obj.transform.Find("CollectibleButton/Text/Name").GetComponent<TextMeshProUGUI>();
            collectibleName.SetText(collectible.itemName);

            TextMeshProUGUI collectibleDesc = obj.transform.Find("CollectibleButton/Text/Description").GetComponent<TextMeshProUGUI>();
            collectibleDesc.SetText(collectible.description);
        }
    }

    public void SetInventory(bool val) {
        if (val) {
            ListItems();
            ButtonNotif.SetActive(false);
        }

        CollectiblesPanel.SetActive(val);
    }

    public void ToggleInventory() {
        SetInventory(!CollectiblesPanel.activeSelf);
    }

    public void SetInventoryBasedOnPlayer(PlayerController player) {
        collectibles.Clear();

        Debug.Log("Checking abilities...");
        Debug.Log("Enabled Dashing: " + player.EnableDashing);
        Debug.Log("Enabled Wall Climbing: " + player.EnableWallClimb);
        Debug.Log("Enabled Dashing: " + player.EnableDoubleJump);

        if (player.EnableDashing) {
            Debug.Log("Added boots");
            collectibles.Add(boots);
        }

        if (player.EnableWallClimb) {
            Debug.Log("Added gloves");
            collectibles.Add(gloves);
        }
        if (player.EnableDoubleJump) {
            Debug.Log("Added winged boots");
            collectibles.Add(wingedBoots);
        }

        if (CollectiblesPanel.activeSelf) {
            ListItems();
        }
    }

    void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Inventory")) { // Joystick Button 2
            ToggleInventory();
        } else if (Input.GetKeyDown(KeyCode.Escape) && CollectiblesPanel.activeSelf) {
            SetInventory(false);
        }
    }
}
