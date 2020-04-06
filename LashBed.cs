using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LashBed : MonoBehaviour
{
    public bool occupied;
    public bool serviceComplete;

    public GameObject occupant;

    public float serviceLengthInSeconds = 5; // TODO: Make abstract class / interface 'Service' -- LashFill : Service, LashFullSet : Service etc..


    private void Start()
    {
        occupied = false;
        
    }

    private void Update()
    {
        //foreach (GameObject client in GameObject.FindGameObjectsWithTag("Client"))
            //Physics2D.IgnoreCollision(GetComponents<Collider2D>()[1], client.GetComponent<Collider2D>());
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogError(collision.collider.name + " hit me!");
        
        if (collision.collider.CompareTag("Player"))
        {
            Player playerScript = collision.collider.GetComponent<Player>();

            playerScript.StopMovement();

            if (occupied)
            {
                StartCoroutine(playerScript.StartService(serviceLengthInSeconds, occupant));
            }
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();

            playerScript.StopMovement();

            if (occupied)
            {
                StartCoroutine(playerScript.StartService(serviceLengthInSeconds, occupant));
            }
        }

        if (other.gameObject.CompareTag("Client"))
        {
            occupant = other.gameObject;
            occupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Client"))
        {
            occupied = false;
            occupant = null;
        }
    }
}
