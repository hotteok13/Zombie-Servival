using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject origin;
    
    [SerializeField] int speed = 5;


    private void Start()
    {
        Destroy(gameObject, 3);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(origin.transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Block"))
        {
            Instantiate(Resources.Load<GameObject>("WFX_BImpact Wood"), transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
        if(other.CompareTag("Enemy")&& !other.CompareTag("Block"))
        {
            
           other.transform.GetComponentInParent<AIControl>().health-=20;
           other.transform.GetComponentInParent<AIControl>().Death();
        }
    }
}
