using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject Nature;
    public GameObject Tech;

    public GameObject NatureBG;
    public GameObject TechBG;

    public PlayerController playerController;

    
    // Update is called once per frame
    void Update()
    {
        if (playerController.levelMode == false)
        {
            Nature.SetActive(true);
            NatureBG.SetActive(true);
            TechBG.SetActive(false);
            Tech.SetActive(false);
        }
        else
        {
            Nature.SetActive(false);
            NatureBG.SetActive(false);
            TechBG.SetActive(true);
            Tech.SetActive(true);
        }
    }
}
