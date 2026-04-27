using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class missle : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    GameObject impact;
    public Transform partToRotate;
    public float missleForce = 30f;
    public int damage = 5;
    public float rotSpeed = 15f;
    public GameObject impactPrefab;
    public GameObject impactDestroy;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }

    void FixedUpdate()
    {
        if (!target)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float minDistanse = 1000;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float Distans = Vector3.Distance(transform.position, transform.position);
                if (Distans < minDistanse)
                {
                    minDistanse = Distans;
                    nearestEnemy = enemy;
                }
            }
            if (enemies.Length == 0f)
            {
                impact = Instantiate(impactDestroy, transform.position, Quaternion.identity);
                Destroy(impact, 1.9f);
                Destroy(gameObject);
            }
            target = nearestEnemy.transform;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * missleForce * Time.deltaTime, Space.World);
        if (target == null)
        { 
        //impact = Instantiate(impactDestroy, transform.position, Quaternion.identity);
        //Destroy(impact, 1.9f);
        //Destroy(gameObject);
            return;
        }
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.LookAt(target);
        
    }
    public void Settarget(Transform _target)
    {
        target = _target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            impact = Instantiate(impactPrefab, transform.position, Quaternion.identity);
            Destroy(impact, 1.9f);
        }
        else
        {
            impact = Instantiate(impactDestroy, transform.position, Quaternion.identity);
            Destroy(impact, 1.9f);
        }
        Destroy(gameObject);
        Collider[] colEnemy = Physics.OverlapSphere(transform.position, 8);
        foreach (Collider item in colEnemy)
        {
            if (item.tag == "Enemy")
            {
                //Destroy(item.gameObject);
                //impact = Instantiate(impactPrefab, transform.position, Quaternion.identity);
                //Destroy(impact, 1.9f);
                Enemy enemy = item.GetComponent<Enemy>();
                enemy.SetDamage(Random.Range(1, damage));
            }
        }
    }
}
