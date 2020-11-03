using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PiggyBankMovement : MonoBehaviour
{
    [SerializeField] bool patrolWaiting;
    [SerializeField] float totalWaitTime = 1f;
    //probability of switching direction
    [SerializeField] float switchProbability = 0.2f;

    NavMeshAgent navMeshAgent;
    ConnectedWaypoint currentWaypoint;
    ConnectedWaypoint previousWaypoint;

    bool travelling;
    bool waiting;
    float waitTimer;
    int waypointsVisited;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("yo navMesh agent not attached to" + gameObject.name);
        } else
        {
            if(currentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if(allWaypoints.Length > 0)
                {
                    while(currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        if (startingWaypoint != null)
                        {
                            currentWaypoint = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to find any waypoints for use in the scene.");
                }
            }

            SetDestination();
        }

    }


    public void Update()
    {
        //check if close to destination
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            waypointsVisited++;

            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                SetDestination();
            }
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                waiting = false;
                SetDestination();
            }
        }
    }


    private void SetDestination()
    {
        if(waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = currentWaypoint.NextWayPoint(previousWaypoint);
            previousWaypoint = currentWaypoint;
            currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = currentWaypoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        travelling = true;
    }
}