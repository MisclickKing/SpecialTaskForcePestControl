using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    private bool collided;
    private float lifeSpan = 0.3f;
    static public float bulletDamage = 10f;

    [SerializeField]
    private GameObject impactEffect;

    private void OnCollisionEnter(Collision c) 
    {

        if(c.gameObject.tag != "Bullet" && c.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            var impact = Instantiate (impactEffect, c.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, 1);
            Destroy(gameObject);
        } 
    }

    private void Update() {
         Destroy(gameObject, lifeSpan);
    }
}
