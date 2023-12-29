using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private bool collided;
    private float lifeSpan = 0.2f;
    static public float flameDamage = 0.75f;

    private void OnCollisionEnter(Collision c) 
    {
        if(c.gameObject.tag != "Flames" && c.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        } 
    }

    private void Update() {
         Destroy(gameObject, lifeSpan);
    }
}
