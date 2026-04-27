using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    Transform target;
    int pointindex = 0;
    public int hp = 1;
    public GameObject prefabImp;
    Renderer rend;
    Gamecontorller gamecontroller;
    public float Hue = 0.7f;
    void Start()
    {
        GameObject Master = GameObject.Find("Master");
        gamecontroller = Master.GetComponent<Gamecontorller>();
        target = WayPoints.points[0];
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (speed < 10)
        {
            speed += 0.1f;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if (dir.magnitude < 0.3f)
        {
            GetNextPoint();
        }
    }
    void GetNextPoint()
    {
        pointindex++;
        if (pointindex >= WayPoints.points.Length)
        {
            EnemyinBase();
            return;
        }
        target = WayPoints.points[pointindex];
    }
    void EnemyinBase()
    {
        Destroy(gameObject);
        gamecontroller.BaseHP -= 1;

    }
    public void SetDamage(int _damage)
    {
        hp -= _damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
           GameObject impact= Instantiate(prefabImp, transform.position, Quaternion.identity);
            Destroy(impact, 1.9f);
            gamecontroller.Money += 1;
        }

        rend.material.color = Color.HSVToRGB(Hue, 1, hp / 10.0f);
    }
}
