using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(1f);
    }
}
