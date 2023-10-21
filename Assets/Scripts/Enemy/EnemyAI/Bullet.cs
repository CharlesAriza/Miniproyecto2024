using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            //How many damage takes the player.
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(30);
        }
        Destroy(gameObject);
    }
}