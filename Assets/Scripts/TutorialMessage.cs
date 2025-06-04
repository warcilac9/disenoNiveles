using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessage : MonoBehaviour
{
    public int tutorialIndex;
    GameManager gameManager;
    Collider2D triggerCollider;

    public bool isDoorTutorial = false;


    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            gameManager = GameManager.instance;
            gameManager.ShowTutorialBanner(tutorialIndex, isDoorTutorial);
            triggerCollider.enabled = false; // Disable the collider to prevent multiple triggers
            // Start the coroutine to show the tutorial message
        }
        
    }

   

    
}
