using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum pickupType { coin,gem,health,controller}

    public pickupType pt;
    [SerializeField] GameObject PickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pt == pickupType.coin)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.IncrementCoinCount();

                Instantiate(PickupEffect, transform.position, Quaternion.identity);

                Destroy(this.gameObject, 0.2f);

            }

        }

        if (pt == pickupType.gem)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.IncrementGemCount();

                Instantiate(PickupEffect, transform.position, Quaternion.identity);

                Destroy(this.gameObject, 0.2f);

            }

        }

        if (pt == pickupType.controller)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.IncrementControllerCount();

                Instantiate(PickupEffect, transform.position, Quaternion.identity);

                Destroy(this.gameObject, 0.2f);

            }
        }
    }
}
