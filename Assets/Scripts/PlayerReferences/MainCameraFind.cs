using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFind : MonoBehaviour
{
    public Camera associatedCamera;
    private void Awake()
    {
        associatedCamera = GetComponent<Camera>();
    }

}
