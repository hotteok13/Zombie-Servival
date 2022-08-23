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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                ObjectPool.instance.InsertQueue(gameObject);
                transform.position = ObjectPool.instance.ActivePosition();
            }
        }
    }

    public void Death()
    {
        if (health <= 0)
        {
            
            agent.speed = 0;
            animator.Play("Death");
            

            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            transform.LookAt(character.transform);
            agent.speed = 0;
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            agent.speed = 3.5f;
            animator.SetBool("Attack", false);
        }
    }
}
