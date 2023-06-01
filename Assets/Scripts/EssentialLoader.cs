using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject UICanvas;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UICanvas).GetComponent<UIFade>();
        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update() { }
}
