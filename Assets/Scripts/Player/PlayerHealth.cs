using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;

    [Header("Health Bar")]
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;
    public float regenerationRate;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;

    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        RegenerateHealth();

        if (overlay.color.a > 0)
        {
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
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        if (fillBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }

        if (fillFront < healthFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            lerpTimer = 0f;
            durationTimer = 0;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        }
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    public void IncreaseHealth(int level)
    {
        maxHealth += ((health * 0.01f) * (100 - level) * 0.1f);
        health = maxHealth;
    }

    public void RegenerateHealth()
    {
        if (health < maxHealth)
        {
            health += regenerationRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0, maxHealth);
            UpdateHealthUI();
        }
    }

    public void Die()
    {
        GameOverMenu gameManager = FindObjectOfType<GameOverMenu>();
        if (gameManager != null)
        {
            Debug.Log("gameManger is not Null");
            gameManager.EndGame();
        }
        else
        {
            Debug.Log("gameManger is Null");
        }
    }
}