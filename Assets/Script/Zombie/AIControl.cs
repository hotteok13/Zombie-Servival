using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private NavMeshAgent agent;
    private Zombie zombie;

    [SerializeField] Transform[ ] Waypoint;

    private int count;
    public int health = 100;

    private Animator animator;
    private Transform target;

    void Start()
    {
        zombie = GetComponent<Zombie>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(MoveNext), 0, 2);
    }

    public void NewTarget(Transform p_Targer)
    {
        CancelInvoke(nameof(MoveNext));
        target = p_Targer;
    }

    public void ResetTarget()
    {
        target = null;
        InvokeRepeating(nameof(MoveNext), 0, 2);
    }

    public void MoveNext()
    {
        if (target == null)
        {
            if (agent.velocity == Vector3.zero)
            {
                agent.SetDestination(Waypoint[count++].position);

                if (count >= Waypoint.Length)
                {
                    count = 0;
                }
            }
        }
        
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
#elif UNITY_WEBGL
        Application.OpenURL("http://google.com");
#else
        Application.Quit();
#endif
    }
    void Update()
    {
        if (zombie.health <= 0)
        {
            CancelInvoke(nameof(MoveNext));
        }
        if (target != null)
        {
            agent.SetDestination(target.position);

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            NewTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            ResetTarget();
        }
    }
}
