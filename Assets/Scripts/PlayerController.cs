using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] LayerMask raycastLayers;
    private NavMeshAgent agent;

    [SerializeField] MultiAimConstraint headAim;

    [SerializeField] GameObject interestingObjects;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, raycastLayers))
            {
                agent.destination = hit.point;
            }

        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (interestingObjects)
        {
            AddNewSource(headAim, interestingObjects.transform, 1f);
        }
        else
        {
            RemoveSource(headAim);
        }
    }

    void AddNewSource(MultiAimConstraint constraint, Transform sourceObject, float sourceWeight)
    {
        WeightedTransform weightedTransform = new WeightedTransform(sourceObject, sourceWeight);
        WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
        sourceObjects.Clear();
        sourceObjects.Add(weightedTransform);
        constraint.data.sourceObjects = sourceObjects;
    }

    void RemoveSource(MultiAimConstraint constraint)
    {
        WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
        sourceObjects.Clear();
        constraint.data.sourceObjects = sourceObjects;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interest"))
        {

            if(interestingObjects != other.gameObject)
            {
                //if(Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, interestingObjects.transform.position))
                //{

                //}
                interestingObjects = other.gameObject;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interest"))
        {

            if (interestingObjects == other.gameObject)
            {
                //if(Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, interestingObjects.transform.position))
                //{

                //}
                interestingObjects = null;
            }

        }
    }
}
