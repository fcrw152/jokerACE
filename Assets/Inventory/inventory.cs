using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    public GameObject bag;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    GameObject slotPanel;
     itemdatabase itemdatabase1;
    // Start is called before the first frame update
    void Start()
    {
        itemdatabase1 = GetComponent<itemdatabase>();
        slotPanel = GameObject.Find("slotpanel");
        for (int i = 0; i < 18; i++)
        {
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].GetComponent<slotdata>().slotID = i;
            items.Add(new Item());
        }
        bag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Additem(int _id)
    {
        Debug.Log("1");
        Item itemToAdd = itemdatabase1.FetchItemByID(_id);
        Debug.Log("3");
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == -1)
            {
                items[i] = itemToAdd;
                GameObject itemobj = Instantiate(inventoryItem);
                itemobj.transform.SetParent(slots[i].transform);
                itemobj.transform.position = Vector2.zero;
                itemobj.name = items[i].Name;
                itemobj.GetComponent<Image>().sprite = itemToAdd.Image;
                itemobj.GetComponent <itemdata>().item= itemToAdd;
                itemobj.GetComponent<itemdata>().slotnum = i;
                break;
            }
        }
    }
}
