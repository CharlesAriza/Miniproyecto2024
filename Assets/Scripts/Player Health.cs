using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour

{

    private float health;
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
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Reinciar la escena si la vida del jugador es menor a =0.
        if (health <= 0)
        {
            StartCoroutine(RestartSceneAfterDelay(0.5f)); // Reiniciar despu�s de 2 segundos (ajusta esto a tu preferencia).
        }
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    TakeDamage(Random.Range(5, 10));
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    RestoreHealth(Random.Range(5, 10));
        //}
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
    public void UpdateHealthUI()
    {
        ;
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
   
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            TakeDamage(40f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(40f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Killwall"))
        {
            TakeDamage(100f);
            Destroy(other.gameObject);
        }
    }

    //Para reinciar la escena.
    IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
