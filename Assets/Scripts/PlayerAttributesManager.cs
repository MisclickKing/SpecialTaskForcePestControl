using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerAttributesManager : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] Slider healthBar;
    [SerializeField] Image healthBarBackground;
    [SerializeField] Image healthBarFill;
    [SerializeField] TMP_Text vitality;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        healthBar.value = currentValue / maxValue;
    }

     public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthBar(health, maxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Full to mostly full health status
        health = maxHealth;
        UpdateHealthBar(health, maxHealth);
        // healthBarBackground.GetComponent<Image>().color = new Color32(49, 221, 194, 36);
        // healthBarFill.GetComponent<Image>().color = new Color32(87, 179, 170, 147);
        // vitality.color = new Color32(0, 255, 232, 134);
    }

    void HealthStatus()
    {
        if(healthBar.value > 0.65f)
        {
            healthBarBackground.GetComponent<Image>().color = new Color32(49, 221, 194, 36);
            healthBarFill.GetComponent<Image>().color = new Color32(87, 179, 170, 147);
            vitality.color = new Color32(0, 255, 232, 134);
        }

        else if(0.22f < healthBar.value && healthBar.value <= 0.65f)
        {
            healthBarBackground.GetComponent<Image>().color = new Color32(213, 221, 49, 36);
            healthBarFill.GetComponent<Image>().color = new Color32(179, 178, 87, 147);
            vitality.color = new Color32(238, 255, 64, 134);
        }
        else if(0.0f < healthBar.value && healthBar.value <= 0.22f)
        {
            healthBarBackground.GetComponent<Image>().color = new Color32(221, 49, 52, 36);
            healthBarFill.GetComponent<Image>().color = new Color32(176, 64, 60, 147);
            vitality.color = new Color32(255, 64, 67, 134);
        }
        else if(healthBar.value <= 0.0f)
        {
            healthBarFill.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            vitality.color = new Color32(255, 64, 67, 134);
            SceneManager.LoadScene("SewerLevel");
        }
    }
    
    void OnTriggerEnter(Collider ce) 
    {
        if(ce.gameObject.tag == "EnemyHitBox")
        {
            TakeDamage(10);
        }

        else if(ce.gameObject.tag == "QueenHitBox")
        {
            TakeDamage(30);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthStatus();
        UpdateHealthBar(health, maxHealth);
    }
}