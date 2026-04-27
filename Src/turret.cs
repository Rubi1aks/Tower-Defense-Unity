using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class turret : MonoBehaviour
{
    public Transform target;
    public string enemyTag = "Enemy";

    public float gunDelay = 1;
    public float range = 15;
    public float rotSpeed = 10f;

    float countDown;

    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, gunDelay);
    }

    void UpdateTarget()
    {
        target = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
           if (Vector3.Distance( transform.position, enemy.transform.position) <= range)
            {
                target = enemy.transform;
                return;
            }
        }
    }
    void Update()
    {
        if(target == null)
           return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        countDown -= Time.deltaTime;
        if(countDown<0)
        {
            Shoot();
            countDown = gunDelay;
        }
    }
    void Shoot()
    {
        if(!target)
        {
            return;
        }
        //partToRotate.LookAt(target);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bullet.transform.LookAt(target);
        missle miss = bullet.GetComponent<missle>();
       
        if (miss != null)
        {
            miss.Settarget(target);
        }
    }
}
