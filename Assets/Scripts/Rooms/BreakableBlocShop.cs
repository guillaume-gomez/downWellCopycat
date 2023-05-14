using UnityEngine;
using System.Collections;
using TMPro;

public class BreakableBlocShop : MonoBehaviour
{
    public SpawnObject spawner;
    public TextMeshPro text;
    public SpriteRenderer platformRenderer;
    private float priceToBeDestroyed;
    private bool canBeBought;

    void Start()
    {
        LevelManager.instance.OnMoneyChange += OnMoneyChange;
        spawner.Init();
        // We assume spawner has script PickupBoughtItem
        PickupBoughtItem script = spawner.gameObject.transform.GetChild(0).GetComponent<PickupBoughtItem>();
        priceToBeDestroyed = script.price;

        text.text = priceToBeDestroyed.ToString();

        CheckColor(GameManager.instance.LevelSystemRun.money);
    }

    void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
    {
        CheckColor(e.money);
    }

    private void CheckColor(float money)
    {
        if(priceToBeDestroyed <= money)
        {
            canBeBought = true;
            text.color = new Color32(255, 255, 255, 255);
            ThemeManager.instance.SetColor(platformRenderer, ColorTypeTheme.Blocs);
        }
        else
        {
            canBeBought = false;
            text.color = new Color32(255, 50, 50, 255);
            ThemeManager.instance.SetColor(platformRenderer, ColorTypeTheme.NoMoneyBloc);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      if(canBeBought)
      {
        if(collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
      }
    }
}