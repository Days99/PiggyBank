using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWaypoint : WaypointScript
{
    [SerializeField] protected float connectivityRadius = 10.0f;
    List<ConnectedWaypoint> connections;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        connections = new List<ConnectedWaypoint>();

        for(int i = 0; i < allWayPoints.Length; i++)
        {
            ConnectedWaypoint nextWaypoint = allWayPoints[i].GetComponent<ConnectedWaypoint>();

            if(nextWaypoint != null)
            {
                if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius && nextWaypoint != this)
                {
                    connections.Add(nextWaypoint);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, connectivityRadius);
    }

    public ConnectedWaypoint NextWayPoint(ConnectedWaypoint previousWaypoint)
    {
        if(connections.Count == 0)
        {
            return null;
        }
        else if(connections.Count == 1 && connections.Contains(previousWaypoint))
        {
            return previousWaypoint;
        }
        else
        {
            ConnectedWaypoint nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, connections.Count);
                nextWaypoint = connections[nextIndex];
            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;
        }
    }
}
