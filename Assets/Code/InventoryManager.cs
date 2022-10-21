using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> items = new List<Item>();
    public Dictionary<string, int> itemCount = new Dictionary<string, int>();

    public List<AbilityCollectible> collectibles = new List<AbilityCollectible>();


    public GameObject InventoryPanel;
    Transform InventoryPanelTrans;
    public GameObject InventorySlotPrefab;

    public GameObject InventoryTooltip;
    TextMeshProUGUI itemHeader;
    TextMeshProUGUI itemDesc;

    public GameObject CollectiblesPanel;
    Transform CollectiblesPanelTrans;
    public GameObject CollectibleSlotPrefab;


    // Start is called before the first frame update
    void Start() {
        instance = this;

        if (InventoryPanel != null) {
            InventoryPanelTrans = InventoryPanel.transform;
        }

        if (InventoryTooltip != null) {
            itemHeader = InventoryTooltip.transform.Find("Header").GetComponent<TextMeshProUGUI>();
            itemDesc = InventoryTooltip.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        }

        if (CollectiblesPanel != null) {
            CollectiblesPanelTrans = CollectiblesPanel.transform;
        }
    }

    public void AddItem(Item item) {
        if (!itemCount.ContainsKey(item.ID)) {
            itemCount[item.ID] = 0; // initialize count
            items.Add(item); // add item
        }

        itemCount[item.ID]++;

        Debug.Log("Added " + item.ID + ", curr qty: " + itemCount[item.ID]);
        
        if (InventoryPanel.activeSelf) {
            ListItems();
        }
    }

    public void AddItem(AbilityCollectible collectible) {
        collectibles.Add(collectible);

        if (InventoryPanel.activeSelf) {
            ListItems();
        }
    }

    public void RemoveItem(Item item) {
        itemCount.Remove(item.ID);
        items.Remove(item);
    }

    public void UseItem(Item item) {
        // use item
        item.Use();

        // update quantity
        itemCount[item.ID]--;
        if (itemCount[item.ID] <= 0) {
            RemoveItem(item);
        }

        // update inventory UI
        ListItems();

        // close tooltip
        InventoryTooltip.SetActive(false);
    }

    public void DisplayItem(Item item) {
        itemHeader.SetText(item.itemName);
        itemDesc.SetText(item.description);

        InventoryTooltip.SetActive(true);
    }

    public void ListItems() {
        foreach (Transform child in InventoryPanelTrans) {
            Destroy(child.gameObject);
        }

        foreach (Item item in items) {
            GameObject obj = Instantiate(InventorySlotPrefab, InventoryPanelTrans);

            Image itemIcon = obj.transform.Find("InventoryButton/Icon").GetComponent<Image>();
            itemIcon.sprite = item.sprite;

            TextMeshProUGUI itemQty = obj.transform.Find("InventoryButton/QuantityText").GetComponent<TextMeshProUGUI>();
            itemQty.SetText(itemCount[item.ID].ToString());

            Button button = obj.transform.Find("InventoryButton").GetComponent<Button>();
            button.onClick.AddListener(delegate { UseItem(item); });

            // tooltip handling
            EventTrigger.Entry eventEnter = new EventTrigger.Entry();
            eventEnter.eventID = EventTriggerType.PointerEnter;
            eventEnter.callback.AddListener( delegate { DisplayItem(item);} );

            EventTrigger.Entry eventExit = new EventTrigger.Entry();
            eventExit.eventID = EventTriggerType.PointerExit;
            eventExit.callback.AddListener(delegate { InventoryTooltip.SetActive(false); } );

            button.gameObject.AddComponent<EventTrigger>();
            EventTrigger buttonEvent = button.gameObject.GetComponent<EventTrigger>();
            buttonEvent.triggers.Add(eventEnter);
            buttonEvent.triggers.Add(eventExit);
        }

        // collectibles list
        foreach (Transform child in CollectiblesPanelTrans) {
            Destroy(child.gameObject);
        }

        foreach (AbilityCollectible collectible in collectibles) {
            GameObject obj = Instantiate(CollectibleSlotPrefab, CollectiblesPanelTrans);
            
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
        } else {
            // always close tooltip if inventory closes
            InventoryTooltip.SetActive(val);
        }

        InventoryPanel.SetActive(val);
        CollectiblesPanel.SetActive(val);
    }

    public void ToggleInventory() {
        SetInventory(!InventoryPanel.activeSelf);
    }

    void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.E)) {
            ToggleInventory();
        } else if (Input.GetKeyDown(KeyCode.Escape) && InventoryPanel.activeSelf) {
            SetInventory(false);
        }
    }
}
