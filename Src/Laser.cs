using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Laser : MonoBehaviour
{
    public Transform target;
    public Transform partToRotate;

    public float rotSpeed = 15f;
    public string enemytag = "Enemy";
    public float range = 10;

    LineRenderer lineRenderer;
    public Transform firePoint;
    public ParticleSystem spark;
    public Light sparkLight;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("SetTarget",0, 0.4f);
        sparkLight.enabled = false;
        spark.Stop();
    }
    void SetTarget()
    {
        target = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float minDistanse = 1000;
        GameObject nearestTarget = null;
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist < minDistanse)
            {
                minDistanse = dist;
                nearestTarget = enemy;
            }
        }
        if(minDistanse <= range)
        {
            target = nearestTarget.transform;           
        }      
        if(target)
        {
            Enemy enemytarget = target.GetComponent<Enemy>();
            enemytarget.speed = 2;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            lineRenderer.enabled = false;
            if(sparkLight.enabled)
            {
                //spark.Stop();
                sparkLight.enabled = false;
            }       
            return;
        }
        Vector3 dir = target.position - transform.position;
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position - dir.normalized * 1.50001f);
        lineRenderer.enabled = true;
        spark.transform.position = target.position - dir.normalized  * 1.50001f;
        spark.transform.rotation = Quaternion.LookRotation(-dir);
        if (!sparkLight.enabled)
        {
            //spark.Play();
            sparkLight.enabled = true;
        }       
    }
}
