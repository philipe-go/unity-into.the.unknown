using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class BombBomb : MonoBehaviour
{
    [SerializeField] private float delay = 5.0f;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float force = 1000f;
    private Animator anim;
    
    private float countDown;
    private bool isExploded = false;
    public GameObject ExplosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f && !isExploded)
        {
            Explosion();
            isExploded = true;
        }
        
    }

    void Explosion()
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (nearbyObject.tag != "PlayerCar" && rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                
                
            }
            
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("Dead");
            Invoke("SpiderDead", 10f);
        }

    }
    void SpiderDead()
    {
        if (gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
