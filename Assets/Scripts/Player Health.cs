using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using Unity.Netcode;
public class PlayerHealth : NetworkBehaviour

{
    public CharacterController characterController; // Agrega una referencia al CharacterController
    public Transform checkpoint;
    public float health;
    //private NetworkVariable<float> health = new NetworkVariable<float>(100f);
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    [Header("Damage Overlay")]
    public float duration;
    public float fadeSpeed;
    public Image overlay;
    private float durationTimer;
    private LivesUIManager livesUIManager;
    private int deathsCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner)
        {
            Debug.Log("Me he iniciado" + transform.name);
        checkpoint = PlayerHelperInicializator.Singleton.checkpoint.GetComponent<Transform>();
        frontHealthBar = PlayerHelperInicializator.Singleton.frontHealthBar.GetComponent<Image>();
        backHealthBar = PlayerHelperInicializator.Singleton.backHealthBar.GetComponent<Image> ();
        overlay = PlayerHelperInicializator.Singleton.overlay.GetComponent<Image>();
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
            livesUIManager = FindObjectOfType<LivesUIManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            UpdateHealthUI();

            if (overlay.color.a > 0)
            {
                if (health < 30)
                    return;

                durationTimer += Time.deltaTime;
                if (durationTimer > duration)
                {
                    float tempAlpha = overlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
                }
            }
        }

    }

    private void UpdateLivesUI()
    {
        // Obt�n el n�mero de vidas restantes
        int vidasRestantes = Mathf.Max(0, 5 - deathsCount);

        // Actualiza la UI usando el LivesUIManager
        if (livesUIManager != null)
        {
            livesUIManager.UpdateLivesUI(vidasRestantes);
        }
    }

    public void UpdateHealthUI()
    {
        if (IsOwner)
        {
            float fillF = frontHealthBar.fillAmount;
            float fillB = backHealthBar.fillAmount;
            float hFraction = health / maxHealth;
            if (fillB > hFraction)
            {
                frontHealthBar.fillAmount = hFraction;
                backHealthBar.color = Color.red;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
            }
            if (fillF < hFraction)
            {
                backHealthBar.color = Color.green;
                backHealthBar.fillAmount = hFraction;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);

        if (health <= 0)
        {
            deathsCount++;
            UpdateLivesUI();
            // Teletransporta al jugador al checkpoint
            TeleportToCheckpoint();
        }
    }


    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (IsOwner)
        {
            if (other.CompareTag("Trap"))
            {
                TakeDamage(40f);
                other.GetComponent<EnemyHealth>().DieServerRPC();
            }

            if (other.CompareTag("Health"))
            {
                RestoreHealth(40f);
                other.GetComponent<PickupRPC>().DestroyPickupServerRPC();
            }

            if (other.CompareTag("Killwall"))
            {
                TakeDamage(100f);
            }
        }
    }
    private void TeleportToCheckpoint()
    {
       if (checkpoint != null)
        {
           // Desactiva el CharacterController temporalmente
            characterController.enabled = false;

           // Establece la posici�n del jugador en la del checkpoint
            transform.position = checkpoint.position;

           // Vuelve a activar el CharacterController
            characterController.enabled = true;

           // Restaura la salud del jugador al m�ximo (si deseas)
            health = maxHealth;
        }
        else
        {
           Debug.LogError("El checkpoint no est� asignado en el script del jugador.");
       }
    }

}