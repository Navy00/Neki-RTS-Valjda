using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionList : MonoBehaviour
{
    public void Move(NavMeshAgent agent, RaycastHit hit, TaskList task)
    {
        agent.destination = hit.point;
        Debug.Log("Moving");
    }
    public void Gather(NavMeshAgent agent, RaycastHit hit, TaskList task)
    {
        agent.destination = hit.collider.gameObject.transform.position;
        Debug.Log("Harvesting");
    }
}
