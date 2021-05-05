using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    public NavMeshAgent myAgent;
    public Transform[] patrolPoints;
    public int destination;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = this.gameObject.GetComponent<NavMeshAgent>();

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(!myAgent.pathPending && myAgent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        myAgent.SetDestination(patrolPoints[destination].position);
        destination = (destination + 1) % patrolPoints.Length;
    }
}
