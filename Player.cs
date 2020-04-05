using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4.0f;
    private Vector3 target;

    public bool Locked { get; set; } = false;

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (!Locked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    public void StopMovement()
    {
        target = transform.position;
    }

    public IEnumerator StartService(float waitTime, GameObject client)
    {
        float count = 0;

        Locked = true;

        client.GetComponent<Client>().currentState = ClientState.BeingServiced;

        while (count < waitTime)
        {
            UpdateServiceMeter(waitTime - count);
            yield return new WaitForSeconds(1f);
            count += 1;
        }

        client.GetComponent<Client>().currentState = ClientState.WantsToExit;
        Locked = false;
    }

    void UpdateServiceMeter(float secondsLeft)
    {
        Debug.LogError(secondsLeft + " seconds left!");
    }
}
