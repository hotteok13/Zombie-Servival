using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject origin;
    [SerializeField] int speed = 5;
    
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(origin.transform.forward * speed * Time.deltaTime);
    }
}
