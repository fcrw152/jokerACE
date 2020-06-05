using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class slotdata : MonoBehaviour,IDropHandler
{
    public int slotID;
    inventory inv;
    public void OnDrop(PointerEventData eventData)
    {
        itemdata dropitem = eventData.pointerDrag.GetComponent < itemdata > ();
        if (inv.items[slotID].ID == -1)
        {
            Debug.Log("`````````````````````");
            inv.items[dropitem.slotnum] = new Item();
            dropitem.slotnum = slotID;
            inv.items[slotID] = dropitem.item;
        }
        else if (dropitem.slotnum != slotID)
        {
            Transform item = transform.GetChild(0);
            item.GetComponent<itemdata>().slotnum = dropitem.slotnum;
            item.transform.SetParent(inv.slots[dropitem.slotnum].transform);
            item.transform.position = item.transform.parent.position;
            inv.items[dropitem.slotnum] = item.GetComponent<itemdata>().item;
            dropitem.slotnum = slotID;
            inv.items[slotID] = dropitem.item;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.Find("itemdatabase").GetComponent<inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
