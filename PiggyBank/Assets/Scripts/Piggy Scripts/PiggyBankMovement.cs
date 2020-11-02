using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PiggyBankMovement : MonoBehaviour
{
    [SerializeField] Transform destination;

    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {

        agent = this.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("yo navMesh agent not attached to" + gameObject.name);
        }
        else
        {
            SetDestination();

        }
    }

    private void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            agent.SetDestination(targetVector);
            Debug.Log(destination.transform.position);
        }
    }

}