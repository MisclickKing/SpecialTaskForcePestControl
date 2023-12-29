using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderNest : MonoBehaviour
{
    [SerializeField] private GameObject self;

    public float health;
    public float maxHealth;

    [SerializeField] EnemyHealthBar healthBar;

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    
    private void die()
    {
        if(health <= 0)
        {
            Destroy(self);
        }
    }

    void OnCollisionEnter(Collision ce) 
    {
        if(ce.gameObject.tag == "Flames")
        {
            TakeDamage(FlameThrower.flameDamage);
        }
    }

    private void Awake() 
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Start()
    {
         healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Update() 
    {
        die();
    }
}
