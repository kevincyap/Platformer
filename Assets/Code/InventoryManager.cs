using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> items = new List<Item>();
    public Dictionary<string, int> itemCount = new Dictionary<string, int>();


    public GameObject InventoryPanel;
    Transform InventoryPanelTrans;
    public GameObject InventorySlotPrefab;


    // Start is called before the first frame update
    void Start() {
        instance = this;

        if (InventoryPanel != null) {
            InventoryPanelTrans = InventoryPanel.transform;
        }
    }

    public void AddItem(Item item) {
        if (!itemCount.ContainsKey(item.ID)) {
            itemCount[item.ID] = 0; // initialize count
            items.Add(item); // add item
        }

        itemCount[item.ID]++;

        Debug.Log("Added " + item.ID + ", curr qty: " + itemCount[item.ID]);
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
        }
    }

    public void SetInventory(bool val) {
        if (val) {
            ListItems();
        }

        InventoryPanel.SetActive(val);
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
