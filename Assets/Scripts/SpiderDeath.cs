using UnityEngine;

public class SpiderDeath : MonoBehaviour
{
    [SerializeField] private Animation animate;

    // Start is called before the first frame update
    void Start()
    {
        animate.Play("Death");   
    }
}
