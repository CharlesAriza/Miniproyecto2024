using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject vfxPrefab; // Asigna el VFX en el inspector
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            //How many damage takes the player.
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(30);
        }
        if (vfxPrefab != null)
        {
            Instantiate(vfxPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}