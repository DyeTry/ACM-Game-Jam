using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScalable : MonoBehaviour
{
    [Header("Health Info")]
    public float health;

    [Header("Currency Info")]
    public int moneyDropOnHit;
    public int moneyDropOnDie;

    [Header("Experience Info")]
    public int enemyLevel;
    public float experience;

    private float lerpTimer;
    private float delayTimer;

    [Header("Multipliers")]
    [Range(1f,300f)]
    public float additionMultipler = 300;
    [Range(2f,4f)]
    public float powerMultipler = 2;
    [Range(7f,14f)]
    public float divisionMultipler = 7;

    private Wave wave;
    private PlayerCurrency playerCurrency;
    private LevelSystem playerLevels;

    public void SetWaveReference(Wave waveReference) {wave = waveReference; }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        playerLevels = FindObjectOfType<LevelSystem>();

        if (playerCurrency != null) playerCurrency.AddMoney(moneyDropOnHit);

        if(health <= 0f) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        if (wave != null) wave.enemiesLeft--;
        if (playerCurrency != null) playerCurrency.AddMoney(moneyDropOnDie);
        if (playerLevels != null) playerLevels.GainExperienceFlatRate(experience);
    }
}