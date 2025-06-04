using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBox : MonoBehaviour
{
    Collider2D triggerCollider;

    public GameObject[] gameObjects;

    public enum Activate {yes, no}
    public Activate Active;

    public enum color {other, green}
    public color colorIs;

    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (colorIs == color.other)
        {
            if (other.CompareTag("Box"))
            {
                if (Active == Activate.yes)
                {
                    foreach (GameObject obj in gameObjects)
                    {
                        obj.SetActive(false);
                    }
                }
                else
                {
                    foreach (GameObject obj in gameObjects)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                if (Active == Activate.yes)
                {
                    foreach (GameObject obj in gameObjects)
                    {
                        obj.SetActive(false);
                    }
                }
                else
                {
                    foreach (GameObject obj in gameObjects)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }

        




            // Disable the collider to prevent multiple triggers
            triggerCollider.enabled = false;
        }
}

