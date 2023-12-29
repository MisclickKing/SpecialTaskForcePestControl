using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAttributes : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] private GameObject deadSelf;
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
            Instantiate(deadSelf, transform.position, transform.rotation);
        }
    }

    void OnCollisionEnter(Collision ce) 
    {

        if(ce.gameObject.tag == "Bullet")
        {
            TakeDamage(NormalBullet.bulletDamage);
        }

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
