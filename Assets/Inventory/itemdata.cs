using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class itemdata : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Item item;
    public int slotnum;
    inventory inv;
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.parent.parent);
        if (item != null) { this. transform.position = eventData.position; }
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null) { this.transform.position = eventData.position; }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (item != null) { this.transform.position = eventData.position; }
        transform.SetParent(inv.slots[slotnum].transform);
        transform.position = transform.parent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
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
