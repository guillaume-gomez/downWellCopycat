using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public string[] inventories;
    public GameObject item;

    void Start()
    {
        Transform itemsParent = GameObject.Find("ItemsParent").transform;
        itemsParent.transform.SetParent(transform);
        
        for(int i = 0; i < inventories.Length; ++i)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            GameObject obj = Instantiate(item, position, transform.rotation);
            obj.transform.SetParent(itemsParent);
        }
    }
}