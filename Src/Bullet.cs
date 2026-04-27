using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [Header("bullet Props")]
    GameObject impact;
    public float bulletForce = 30;
    public int damage = 1;

    public GameObject impactPrefab;
    public GameObject impactDestroy;
    
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.SetDamage(damage);
            impact = Instantiate(impactPrefab, transform.position, Quaternion.identity);
            Destroy(impact, 0.5f);
        }
        else
        {
            impact = Instantiate(impactDestroy, transform.position, Quaternion.identity);
            Destroy(impact, 1.9f);
        }
        Destroy(gameObject);
    }
}
