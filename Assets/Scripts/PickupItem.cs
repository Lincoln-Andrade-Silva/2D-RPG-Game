using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickup;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (canPickup && PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().name);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            canPickup = false;
        }
    }
}
