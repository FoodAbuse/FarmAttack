using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerHealthBehaviour : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth; 
    public Image healthSlider;
    public TMP_Text healthText; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
    }
    private void Update()
    {
        UpdateHealthUI();

    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0)
            currentHealth = 0; 
        UpdateHealthUI();
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth; 
        UpdateHealthUI(); 
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.fillAmount = currentHealth / maxHealth; 

        if (healthText != null)
            healthText.text = currentHealth.ToString("0");
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
