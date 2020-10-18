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
        priceToBeDestroyed = script.price;
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