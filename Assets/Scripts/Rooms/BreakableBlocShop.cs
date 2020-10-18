using UnityEngine;
using System.Collections;

public class BreakableBlocShop : MonoBehaviour
{
    public SpawnObject spawner;
    private float priceToBeDestroyed;

    void Start()
    {
        spawner.Init();
        // We assume spawner has script PickupBoughtItem
        PickupBoughtItem script = spawner.gameObject.transform.GetChild(0).GetComponent<PickupBoughtItem>();
        Debug.Log(priceToBeDestroyed);
        priceToBeDestroyed = script.price;
        Debug.Log(priceToBeDestroyed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      if(priceToBeDestroyed < GameManager.instance.LevelSystemRun.money) {
        if(collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
      }
    }
}