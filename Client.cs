using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public float speed = 4.0f;

    private Vector3 target;
    private GameObject targetObject;

    public ServiceType serviceType;
    public ClientState currentState;

    //private bool lookingForTarget;

    // Start is called before the first frame update
    private void Start()
    {
        //lookingForTarget = false;
        serviceType = GetRandomServiceType();
        currentState = ClientState.WaitingForSpawn;
        target = FindWaitingArea();
        Debug.Log(serviceType);
    }

    // Update is called once per frame
    private void Update()
    {
        if ((currentState == ClientState.WantsToMove || currentState == ClientState.MovingToTarget)
            && (target != null && target != transform.position))
        {
            MoveToTarget();
        }

        else if (currentState == ClientState.WaitingForSpawn)
        {
            target = FindWaitingArea();
        }

        else if (currentState == ClientState.WantsToExit)
        {
            target = FindExit();

            if (transform.position == target)
            {
                Destroy(this);
            }
        }
    }

    private void MoveToTarget()
    {
        if (currentState == ClientState.WantsToMove)
            currentState = ClientState.MovingToTarget;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //Debug.Log(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (currentState == ClientState.WaitingForService)
            {
                Debug.LogError("Looking for service area");
                target = FindServiceArea();
            }
        }

        else if (other.gameObject.CompareTag("WaitingArea"))
        {
                currentState = ClientState.WaitingForService;
                Debug.Log(currentState);
        }
    }

    private Vector3 FindWaitingArea()
    {
        //lookingForTarget = true;

        Vector3? res = null;

        GameObject[] waitingAreas = GameObject.FindGameObjectsWithTag("WaitingArea");

            foreach (GameObject waitingArea in waitingAreas)
            {
                Debug.LogWarning("waiting area in loop: " + waitingArea + "occupied?: " + waitingArea.GetComponent<WaitingArea>().occupied);
                if (!waitingArea.GetComponent<WaitingArea>().occupied)
                {
                    waitingArea.GetComponent<WaitingArea>().occupied = true;
                    targetObject = waitingArea;
                    res = waitingArea.transform.position;
                    break;
                }
            }

        if (res != null)
            currentState = ClientState.WantsToMove;
        else
        {
            Debug.LogError("No available targets. (waitingArea)");
            return transform.position;
        }

        //lookingForTarget = false;

        return (Vector3)res;
    }

    private Vector3 FindServiceArea()
    {
        //lookingForTarget = true;

        Vector3? res = null;

        if (serviceType == ServiceType.FullSet || serviceType == ServiceType.LashLift)
        {
            GameObject[] lashBeds = GameObject.FindGameObjectsWithTag("LashBed");

            //while (res == null)
            //{
                foreach (GameObject lashBed in lashBeds)
                {
                    if (!lashBed.GetComponent<LashBed>().occupied)
                    {
                        Debug.LogError(lashBed);
                        lashBed.GetComponent<LashBed>().occupied = true;
                        targetObject = lashBed;
                        res = lashBed.transform.position;
                        break;
                    }
                }
            //}
        }

        if (res != null)
            currentState = ClientState.WantsToMove;
        else
        {
            Debug.LogError("No available targets. (serviceArea)");
            return transform.position;
        }

        Debug.LogError("Service area found: " + res);
        //lookingForTarget = false;
        return (Vector3)res;
    }

    private Vector3 FindExit()
    {
        // -8.5, -2.25, 0.0


    }

    private ServiceType GetRandomServiceType()
    {
        ServiceType res = ServiceType.FullSet;

        int random = Random.Range(0, System.Enum.GetNames(typeof(ClientState)).Length - 1);

        switch (random)
        {
            case 0:
                res = ServiceType.FullSet;
                break;
            case 1:
                res = ServiceType.LashLift;
                break;
        }

        Debug.LogError("Client ServiceType: " + res);
        return res;
    }
}
