using System;
using UnityEngine;

public class objPush : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private GameObject textPush;
    private float playerMass; // Get the mass of the object
    float acceleration;
    private bool enter = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            textPush.SetActive(true);
        }
        if ( Input.GetKeyDown(KeyCode.F) && other.gameObject.CompareTag("Player"))
        {
            playerMass = other.gameObject.GetComponent<Rigidbody>().mass;
            acceleration = other.gameObject.GetComponent<CharacterMovement>().moveSpeed / rb.mass;
            enter = true;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.gameObject.CompareTag("Player"))
        {
            playerMass = other.gameObject.GetComponent<Rigidbody>().mass;
            acceleration = other.gameObject.GetComponent<CharacterMovement>().moveSpeed / rb.mass;
            enter = true;
        }
    }

    private void Update()
    {
        if (enter)
        {
            Debug.Log("Pushing object");
            
            rb.AddForce(-transform.forward *(rb.mass * acceleration ) , ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        enter = false;
        textPush.SetActive(false);
    }
}
