
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance { get; private set; }

    [SerializeField] private List<Item> items;

    private Dictionary<int, Item> itemLookup;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeItemLookup();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void InitializeItemLookup()
    {
        itemLookup = new Dictionary<int, Item>();

        foreach (var item in items)
        {
            if (item != null)
            {
                if (!itemLookup.ContainsKey(item.itemID))
                {
                    itemLookup.Add(item.itemID, item);
                }
                
            }
        }
    }

    public Item ReturnItem(int id)
    {
        if (id == -1) return null;

        if (itemLookup.TryGetValue(id, out var item))
        {
            return item;
        }
       // Debug.Log($"Item with ID {id} not found!");
        return null;
    }





}
