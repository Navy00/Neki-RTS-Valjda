﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //nav mash

public class ObjectInfo : MonoBehaviour
{
    public TaskList task;
    public ResourceManager RM;

    GameObject targetNode; //node iz koje se majnuje trenutno

    public NodeManager.ResourceType heldResourceType;
    public bool isSelected = false;
    public bool isGathering = false;

    public string objectName;

    private NavMeshAgent agent;

    public int heldResource;
    public int maxHeldResource;

    public GameObject[] drops;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();//typeof(NavMashAgent)
    }

    // Update is called once per frame
    void Update()
    {
        if(targetNode == null)
        {
            if(heldResource != 0)
            {
                drops = GameObject.FindGameObjectsWithTag("Drop");
                agent.destination = GetClosestDropOff(drops).transform.position;
                drops = null;
                task = TaskList.Delivering;
            }
            else
            {
                task = TaskList.Idle;
            }
        }
        if(Input.GetMouseButtonDown(1) && isSelected)
        {
            RightClick();
        }
        if(heldResource >= maxHeldResource)
        {
            //Drop off point here
            drops = GameObject.FindGameObjectsWithTag("Drop");
            agent.destination = GetClosestDropOff(drops).transform.position;
            drops = null;
            task = TaskList.Delivering;
        }
    }
    private GameObject GetClosestDropOff(GameObject[] drops)
    {
        GameObject closestDrop = null;
        float closestDist = Mathf.Infinity;
        Vector3 position = transform.position; // transform.position uzima poziciju objekta koji ima ObjectInfo component u sebi
        foreach(GameObject g in drops)
        {
            Vector3 direction = g.transform.position - position;
            float distance = direction.sqrMagnitude;
            if (distance < closestDist)
            { 
                closestDist = distance;
                closestDrop = g;
            }
        }
        return closestDrop;
    }


    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//gde je mis
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000))//100-dokle da proverava
        {
            if(hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
                Debug.Log("Moving");
                task = TaskList.Moving;
            }
            else if(hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("Harvesting");
                task = TaskList.Gathering;
                targetNode = hit.collider.gameObject;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.tag == "Resource" && task == TaskList.Gathering)
        {
            isGathering = true;
            hitObject.GetComponent<NodeManager>().gatherers++;
            heldResourceType = hitObject.GetComponent<NodeManager>().resourceType;
        }
        else if(hitObject.tag == "Drop" && task == TaskList.Delivering)
        {
            if (RM.Gold >= RM.maxGold)
            {
                task = TaskList.Idle;
            }
            else
            {
                RM.Gold += heldResource;
                heldResource = 0;
                task = TaskList.Gathering;
                agent.destination = targetNode.transform.position;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if (hitObject.tag == "Resource")
        {
            hitObject.GetComponent<NodeManager>().gatherers--;
            isGathering = false;
        }
    }
    IEnumerator GatherTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering && heldResource < maxHeldResource)
            {
                heldResource++;
            }

        }
    }
}
