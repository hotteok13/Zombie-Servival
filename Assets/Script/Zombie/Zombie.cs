using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public int health;

    
    private Animator animator;
    private GameObject character;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.Find("Character");
        agent = GetComponent<NavMeshAgent>();
        
        

    }

    

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(character.transform.position);
    }

    public void Death()
    {
        if (health <= 0)
        {
            
            agent.speed = 0;
            animator.Play("Death");
            transform.position = ObjectPool.instance.ActivePosition();
            ObjectPool.instance.InsertQueue(gameObject);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    ObjectPool.instance.InsertQueue(gameObject);
                }
            }
        }
    }
}
