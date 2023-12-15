using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(1);
            //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            Destroy(gameObject);

        }
        if (coll.gameObject.CompareTag("Enemy"))
        {
            coll.gameObject.GetComponent<Enemy>().TakeDamage(1);
            //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            Destroy(gameObject);

        }

    }

}
