using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;

    public List<AbilityCollectible> collectibles = new List<AbilityCollectible>();

    public GameObject CollectiblesPanel;
    public GameObject CollectiblesList;
    Transform CollectiblesListTrans;
    public GameObject CollectibleSlotPrefab;

    public Button CollectiblesButton;
    public GameObject ButtonNotif;


    // Start is called before the first frame update
    void Start() {
        instance = this;

        if (CollectiblesList != null) {
            CollectiblesListTrans = CollectiblesList.transform;
        }

        CollectiblesButton.onClick.AddListener(delegate { ToggleInventory(); });
    }

    public void AddItem(AbilityCollectible collectible) {
        collectibles.Add(collectible);

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

    void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.E)) {
            ToggleInventory();
        } else if (Input.GetKeyDown(KeyCode.Escape) && CollectiblesPanel.activeSelf) {
            SetInventory(false);
        }
    }
}
