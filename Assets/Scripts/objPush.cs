using System;
using UnityEngine;

public class objPush : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private GameObject textPush;
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
        
        if (other.gameObject.CompareTag("Player")&& Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pushing object");
            // float playerMass = other.gameObject.GetComponent<Rigidbody>().mass; // Get the mass of the object
            // float acceleration = other.gameObject.GetComponent<CharacterMovement>().moveSpeed / objectMass;
            rb.AddForce(-transform.forward * 50f, ForceMode.Impulse);
            
        }
    }
    private void OnCollisionStay(Collision other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            textPush.SetActive(true);
        }
        if (other.gameObject.CompareTag("Player")&& Input.GetKeyDown(KeyCode.F))
        {
            // float objectMass = pullingObjectRB.mass; // Get the mass of the object
            // float acceleration = moveSpeed / playerMass;
            Debug.Log("Pushing object");
            rb.AddForce(-transform.forward * 50f, ForceMode.Impulse);
            
        }
    }
    private void OnCollisionExit(Collision other)
    {
        textPush.SetActive(false);
    }
}
