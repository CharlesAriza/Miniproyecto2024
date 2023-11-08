using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    private Transform playerTransform;
    private bool isGrounded;

    void Start()
    {
        playerTransform = transform;
    }

    void Update()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, Vector3.down, out hit, 1.0f))
        {
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                playerTransform.SetParent(hit.transform);
                isGrounded = true;
            }
            else if (isGrounded)
            {  
                playerTransform.SetParent(null);
                isGrounded = false;
            }
        }
        else if (isGrounded)
        {   
            playerTransform.SetParent(null);
            isGrounded = false;
        }
    }
}
