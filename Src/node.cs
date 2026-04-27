using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class node : MonoBehaviour
{
    Material nodeMat;
    public Color hoverColor;
    public Color busyColor;
    public Color busyColorDown;
    Color startColor;

    GameObject Master;
    public GameObject towerPrefab;
    public Vector3 offset;

    bool canPlace = true;

    Gamecontorller gameContrl;
    void Start()
    {
        nodeMat = GetComponent<Renderer>().material;
        startColor = nodeMat.color;
        Master = GameObject.Find("Master");
        gameContrl = Master.GetComponent<Gamecontorller>();
    }
    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!canPlace)
        {
            //Destroy(towerPrefab.gameObject);
            //canPlace = true;
            //hoverColor = startColor;
            //nodeMat.color = hoverColor;
            return;
        }
        if (gameContrl.Money < gameContrl.towerCost)
        {
            return;
        }
        towerPrefab = gameContrl.TowerPrefab;
        Instantiate(towerPrefab, transform.position + offset, Quaternion.identity);
        canPlace = false;
        gameContrl.Money -= gameContrl.towerCost;

        hoverColor = busyColor;
        nodeMat.color = hoverColor;
    }
        //Input.GetKeyDown(KeyCode.Escape)
    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else
        {
            nodeMat.color = startColor;
        }
        if (!canPlace)
        {
            hoverColor = busyColorDown;
        }
        nodeMat.color = hoverColor;   
    }
    private void OnMouseExit()
    {
        if (!canPlace)
        {
            startColor = busyColor;
        }
        nodeMat.color = startColor;
    }
}
