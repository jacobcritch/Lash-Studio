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

    public IEnumerator StartService(float waitTime, GameObject lashBed)
    {
        float count = 0;

        Locked = true;

        lashBed.GetComponent<LashBed>().serviceInProgress = true;
        while (count < waitTime)
        {
            UpdateServiceMeter(waitTime - count);
            yield return new WaitForSeconds(1f);
            count += 1;
        }

        lashBed.GetComponent<LashBed>().serviceInProgress = false;
        Locked = false;
    }

    void UpdateServiceMeter(float secondsLeft)
    {
        Debug.LogError(secondsLeft + " seconds left!");
    }
}
