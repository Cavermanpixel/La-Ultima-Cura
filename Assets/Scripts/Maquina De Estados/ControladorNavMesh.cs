using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorNavMesh : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void CambiarTrayectoria(Vector3 trayecto)
    {
        agent.SetDestination(trayecto);
        if (agent.isStopped)
        {
            agent.isStopped = true;
        }
    }

    public void CambiarTrayectoria()
    {
        agent.SetDestination(target.position);
    }

    public void DetenerNavMesh()
    {
        if (!agent.isStopped)
            agent.isStopped = true;
    }

    public bool LlegoATrayecto()
    {
        return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
    }
}
