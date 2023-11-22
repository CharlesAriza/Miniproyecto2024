using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHelperInicializator : MonoBehaviour
{

    private static PlayerHelperInicializator singleton;
    public static PlayerHelperInicializator Singleton
    {
        get { return singleton; }
    }


    public pointSphereFind PointSphere;
    public MainCameraFind MainCamera;
    public aimCameraFind aimCamera;
    public checkPointFind checkpoint;
    public frontHealthBarFind frontHealthBar;
    public backHealthBarFind backHealthBar;
    public overlayFind overlay;
    public playerInteractFind playerInteract;
    public currentBulletsFind currentBullets;
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera playerAimCamera;
    public GameObject bulletPrefab;
    public spawnBulletPositionFind spawnBulletPosition;

    private void Awake()
    {
        Debug.Log("Me he iniciadoooooooo");
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        spawnBulletPosition = GameObject.FindAnyObjectByType<spawnBulletPositionFind>();
        PointSphere = GameObject.FindAnyObjectByType<pointSphereFind>();
        MainCamera = GameObject.FindAnyObjectByType<MainCameraFind>();
        aimCamera = GameObject.FindAnyObjectByType<aimCameraFind>();
        checkpoint = GameObject.FindAnyObjectByType<checkPointFind>();
        frontHealthBar = GameObject.FindAnyObjectByType<frontHealthBarFind>();
        backHealthBar = GameObject.FindAnyObjectByType<backHealthBarFind>();
        overlay = GameObject.FindAnyObjectByType<overlayFind>();
        playerInteract = GameObject.FindAnyObjectByType<playerInteractFind>();
        currentBullets = GameObject.FindAnyObjectByType<currentBulletsFind>();
    }

}
