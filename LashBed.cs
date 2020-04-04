using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LashBed : MonoBehaviour
{
    public bool occupied;

    public GameObject occupant;

    public bool serviceInProgress;

    public float serviceLengthInSeconds = 5; // TODO: Make abstract class / interface 'Service' -- LashFill : Service, LashFullSet : Service etc..


    private void Start()
    {
        occupied = false;
    }

    private void Update()
    {
        if (serviceInProgress == false &&  == true)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogError(collision.collider.name + " hit me!");

        if (collision.collider.CompareTag("Player"))
        {
            Player playerScript = collision.collider.GetComponent<Player>();

            playerScript.StopMovement();

            if (occupied)
            {
                occupant.GetComponent<Client>().currentState = ClientState.BeingServiced;
                StartCoroutine(playerScript.StartService(serviceLengthInSeconds, this.gameObject));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Client"))
        {
            occupied = true;
            occupant = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Client"))
        {
            occupied = false;
        }
    }
}
