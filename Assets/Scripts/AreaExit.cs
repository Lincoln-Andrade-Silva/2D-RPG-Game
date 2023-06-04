using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public static AreaExit instance;
    public string areaToLoad;
    public string areaTransitionName;
    public float waitToLoad;

    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.tag == "Player")
        {
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();

            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

    public void LoadSceneFromLoadButton()
    {
        areaToLoad = PlayerPrefs.GetString("Current_Scene");
        shouldLoadAfterFade = true;
        GameManager.instance.fadingBetweenAreas = true;
        UIFade.instance.FadeToBlack();
    }
}
