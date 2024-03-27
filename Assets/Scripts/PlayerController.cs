using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

using UnityEngine.AI;

using UnityEngine.Animations;

using UnityEngine.Animations.Rigging;
 
public class PlayerController : MonoBehaviour

{

    [SerializeField] Animator animator;

    private NavMeshAgent agent;

    [SerializeField] LayerMask raycastLayers;

    [SerializeField] MultiAimConstraint headAim;

    [SerializeField] GameObject interestingObject;


    Vector3 focusObjectOrigPos;

    [SerializeField] Transform focusObject;


    private void Awake()

    {

        agent = GetComponent<NavMeshAgent>();

        focusObjectOrigPos = focusObject.position;

    }

    private void Update()

    {

        if (Input.GetMouseButtonDown(0))

        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, raycastLayers))

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


        if (interestingObject)

        {

            focusObject.position = interestingObject.transform.position;

        }

        else

        {

            focusObject.position = focusObjectOrigPos;

        }

    }



    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.CompareTag("Interest"))

        {

            if (interestingObject != other.gameObject)

            {

                interestingObject = other.gameObject;

            }

        }

    }

    private void OnTriggerExit(Collider other)

    {

        if (other.gameObject.CompareTag("Interest"))

        {

            if (interestingObject == other.gameObject)

            {

                interestingObject = null;

            }

        }

    }

}
