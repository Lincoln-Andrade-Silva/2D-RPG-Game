using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivator : MonoBehaviour
{
    private bool canActivate;

    [Header("Items")]
    public string[] itemsForSale;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (
            canActivate
            && Input.GetKeyDown(KeyCode.E)
            && PlayerController.instance.canMove
            && !Shop.instance.shopMenu.activeInHierarchy
        )
        {
            Shop.instance.itemsForSale = itemsForSale;
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canActivate = false;
        }
    }
}
