﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public enum ResourceType{Gold}
    public ResourceType resourceType;

    public float harvestTime;
    public float availableResource;

    public int gatherers;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResourceTick());
    }

    // Update is called once per frame
    void Update()
    {
        if(availableResource <= 0)
        {
            Destroy(gameObject);//objekat koji ima ovaj script
        }
        //ResourceGather();
    }

    public void OnTriggerEnter(Collider other)
    {
        gatherers++;
    }

    public void OnTriggerExit(Collider other)
    {
        gatherers--;
    }

    public void ResourceGather()
    {
        if(gatherers != 0)
        {
            availableResource -= gatherers;
        }
    }
    IEnumerator ResourceTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            ResourceGather();
        }
    }


}
