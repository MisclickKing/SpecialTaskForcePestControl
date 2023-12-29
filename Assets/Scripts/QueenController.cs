using UnityEngine;
using UnityEngine.AI;

public class QueenController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float attackTimer = 1.45f;
    public float attackTime = 0.5f;
    public float timeLeft;

    [SerializeField] private Animation animate;
    [SerializeField] private GameObject hitbox;
    

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        timeLeft = attackTimer; 
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Attack State
        if (distance <= agent.stoppingDistance)
        {
            // Attack the target
            animate.Play("Attack");

            if(timeLeft > 0){
                timeLeft -= Time.deltaTime;

                if(timeLeft <= attackTime){
                    hitbox.GetComponent<BoxCollider>().enabled = true;
                }

                if(timeLeft <= 0)
                {
                    hitbox.GetComponent<BoxCollider>().enabled = false;
                    timeLeft = attackTimer;
                }
            }

            // Face the target
            FaceTarget();
            
        }
        // Chase State
        else if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animate.Play("Run");
            hitbox.GetComponent<BoxCollider>().enabled = false;
            timeLeft = attackTimer;
        }
        // Idle state
        else 
        {
            animate.Play("Idle");
            hitbox.GetComponent<BoxCollider>().enabled = false;
            timeLeft = attackTimer;
        }
    }

    void FaceTarget() 
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
