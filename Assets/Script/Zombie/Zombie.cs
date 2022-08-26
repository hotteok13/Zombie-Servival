using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float health;

    [SerializeField] float maxhealth=100;
    
    private Animator animator;
    private GameObject character;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.Find("Character");
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        health = 20;
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceSensor();
        float tempHealth = 1 - (health / maxhealth);
        animator.SetLayerWeight(animator.GetLayerIndex("Other Layer"), tempHealth);

        agent.SetDestination(character.transform.position);
        if (health <= 0)
        {
            agent.speed = 0;
            animator.Play("Death");
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    GameManager.instance.count++;
                    ObjectPool.instance.InsertQueue(gameObject);
                    transform.position = ObjectPool.instance.ActivePosition();
                }
            }
            else
            {
                agent.speed = 50f;
                agent.SetDestination(character.transform.position);
            }
        }
    }

    public void DistanceSensor()
    {
        if (Vector3.Distance(character.transform.position, transform.position) <= 2)
        {
            agent.speed = 0;
            transform.LookAt(character.transform);
            animator.SetBool("Attack", true);
        }
        else
        {
            agent.speed = 20;
            animator.SetBool("Attack", false);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    
                    character.GetComponent<Control>().ScreenCall();
                    character.GetComponent<Control>().health -= 20;
                    animator.Rebind();

                }
            }
        }
        
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            agent.speed = 50f;
            animator.SetBool("Attack", false);
        }
    }
}
