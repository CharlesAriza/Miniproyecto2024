using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System;
using TMPro;

public class ThirdPersonShooterController : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
   

    [Header("Bullet Configuration")]
    [SerializeField] int maxBullet = 100;
    [SerializeField] int currentBullets;
    [SerializeField] TMPro.TextMeshProUGUI currentBulletsText;

    [SerializeField] int initialBullets = 2;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentBulletsText = PlayerHelperInicializator.Singleton.currentBullets.GetComponent<TextMeshProUGUI>();
        debugTransform = PlayerHelperInicializator.Singleton.PointSphere.transform;
        aimVirtualCamera = PlayerHelperInicializator.Singleton.aimCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        currentBulletsText.text = currentBullets.ToString();
    }


    private void Update()
    {
        if (!IsOwner) { return; }

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = PlayerHelperInicializator.Singleton.MainCamera.associatedCamera.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            //Ponemos el starterAssetsInput.shoot dentro del aim para que no se pueda disparar si no se esta apuntando.
            if (starterAssetsInputs.shoot && currentBullets > 0)
            {
                currentBullets--;
                currentBulletsText.text = currentBullets.ToString();


                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                starterAssetsInputs.shoot = false;
            }
        }


        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }


        /*if (starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir,Vector3.up));
            starterAssetsInputs.shoot = false;
        }*/

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullets"))
        {
            // Comprueba si el jugador ha entrado en contacto con un objeto con el tag "Bullets".
            // A continuación, agrega balas hasta llegar a 100 o el máximo que permitas.
            int bulletsToAdd = Mathf.Min(100 - currentBullets, 10); // Calcula la cantidad de balas a agregar sin superar 100.
            currentBullets += bulletsToAdd;
            currentBulletsText.text = currentBullets.ToString();

            // Desactiva el objeto recolector de balas para que no pueda ser recogido nuevamente.
            other.gameObject.SetActive(false);
        }
    }
    public void AddBullets(int bulletsToadd)
    {
        currentBullets += bulletsToadd;
        currentBulletsText.text = currentBullets.ToString();

    }


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullets"))
        {
            // Comprueba si el jugador ha entrado en contacto con un objeto con el tag "Bullets".
            // A continuación, agrega 25 balas a la cantidad actual de balas del jugador.
            currentBullets += 10;
            currentbulletsText.text = currentBullets.ToString();

            // Desactiva el objeto recolector de balas para que no pueda ser recogido nuevamente.
            other.gameObject.SetActive(false);
        }
    }*/

}
