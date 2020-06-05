using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
public class itemdatabase : MonoBehaviour
{
    private JsonData itemdata;
    private List<Item> datebase = new List<Item>();
    // Start is called before the first frame update
    void Awake()
    {
        itemdata = JsonMapper.ToObject(File.ReadAllText(Application.dataPath+"/items.json"));
        constructItemdatebase();
        Debug.Log(datebase[0].Description);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void constructItemdatebase()
    {
        for(int i = 0; i < itemdata.Count; i++)
        {
            datebase.Add(new Item((int)itemdata[i]["id"], itemdata[i]["name"].ToString(), (int)itemdata[i]["sellmoney"], 
               (int) itemdata[i]["maxhandnum"], itemdata[i]["type"].ToString(), (int)itemdata[i]["atk"], itemdata[i]["description"].ToString(),itemdata[i]["image"].ToString(),(int)itemdata[i]["iscanrush"]));
        }
    }
    public Item FetchItemByID(int _id)
    {
        Debug.Log("2");
        for (int i = 0; i < datebase.Count; i++)
        {
            if (_id == datebase[i].ID)
            {
                return datebase[i];
            }
        }
        return null;
    }
   
}
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Sellmoney { get; set; }
        public int Handnum { get; set; }
        public string Type { get; set; }
        public int Atk { get; set; }
        public string Description { get; set; }
        public Sprite Image { get; set; }
        public int Iscanrush { get; set; }
        public Item(int _id, string _name, int _sellmoney, int _maxhandnum, string _type, int _atk, string _description,string _image,int _iscanrush)
        {
            ID = _id;
            Name = _name;
            Sellmoney = _sellmoney;
            Handnum = _maxhandnum;
            Type = _type;
            Atk = _atk;
            Description = _description;
        Image = Resources.Load<Sprite>("slot ui/" + _image);
        Iscanrush = _iscanrush;
        }
        public Item()
        {
            ID = -1;
        }
    }