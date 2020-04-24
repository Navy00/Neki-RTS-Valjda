using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //nav mash

public class ObjectInfo : MonoBehaviour
{
    public NodeManager.ResourceType heldResourceType;
    public bool isSelected = false;
    public bool isGathering = false;

    public string objectName;

    private NavMeshAgent agent;

    public int heldResource;
    public int maxHeldResource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();//typeof(NavMashAgent)
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && isSelected)
        {
            RightClick();
        }
        if(heldResource >= maxHeldResource)
        {
            //heldResourceType = null;
            //Drop off point here
        }
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
            }
            else if(hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("Harvesting");
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.tag == "Resource")
        {
            isGathering = true;
            heldResourceType = hitObject.GetComponent<NodeManager>().resourceType;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if (hitObject.tag == "Resource")
        {
            isGathering = false;
        }
    }
    IEnumerator GatherTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering)
            {
                heldResource++;
            }

        }
    }
}
