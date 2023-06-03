using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string name;
    public string[] lines;
    public bool isNPC;

    private bool canActivate;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (
            canActivate
            && Input.GetKeyDown(KeyCode.E)
            && !DialogManager.instance.dialogBox.activeInHierarchy
        )
        {
            DialogManager.instance.ShowDialog(name, lines, isNPC);
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
