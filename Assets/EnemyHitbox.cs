using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Collider2D hitboxCollider;

    public Animator animator;



    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.Death();
        }
        else if (other.CompareTag("Box"))
        {
            StartCoroutine(EnemyDies());
        }
    }

    public IEnumerator EnemyDies()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(0.250f);
        Destroy(transform.parent.gameObject);
        
    }
}
