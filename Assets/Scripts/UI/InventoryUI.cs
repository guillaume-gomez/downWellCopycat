using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] bonusItems;

    void Start()
    {
        Transform itemsParent = GameObject.Find("ItemsParent").transform;
        for(int i = 0; i < bonusItems.Length; ++i)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            GameObject obj = Instantiate(bonusItems[i], position, transform.rotation);
            obj.transform.SetParent(itemsParent);
        }
    }
}