using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
            obj.transform.SetParent(itemsParent, false);
            // select the first item in event
            if(i == 0)
            {
                obj.GetComponent<Selectable>().OnSelect(null);
                EventSystem.current.SetSelectedGameObject(obj);
            }
        }
    }
}