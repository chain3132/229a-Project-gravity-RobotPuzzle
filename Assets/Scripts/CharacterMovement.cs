using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cam;
    public Animator animator;
    public float jumpForce = 2f;
    private float turnSmoothVelocity;
    public Transform handPosition;
    
    
    private Rigidbody rb;
    private bool isPulling = false;
    private bool isGrounded;
    private bool isJumping;
    private FixedJoint joint; // Joint to attach to objects
    private Rigidbody pullingObjectRB;
    private float playerMass;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMass = rb.mass; 
    }

    void Update()
    {
        MoveCharacter();
        Jump();
        // PullObject();
        HandlePulling();
        
    }

    private void FixedUpdate()
    {
        // ApplyGravity();
    }

    // private void PullObject()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         if (isPulling)
    //         {
    //             isPulling = false;
    //             animator.SetBool("isPulling", false);
    //             return;
    //         }
    //         isPulling = true;
    //         animator.SetTrigger("Pulling");
    //         animator.SetBool("isPulling", true);
    //     }
    // }
    void HandlePulling()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPulling)
            {
                TryAttachToBox();
            }
            else
            {
                ReleaseBox();
            }
        }
    }

    void TryAttachToBox()
    {
        Collider[] hitColliders = Physics.OverlapSphere(handPosition.transform.position, 1f); // Detect objects near player
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Box"))
            {
                Rigidbody boxRB = col.GetComponent<Rigidbody>();
                if (boxRB != null)
                {
                    pullingObjectRB = boxRB;
                    // Create joint to attach box to player
                    joint = pullingObjectRB.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = rb;
                    isPulling = true;
                    animator.SetTrigger("Pulling");
                    animator.SetBool("isPulling", true);
                    return;
                }
            }
        }
    }

    void ReleaseBox()
    {
        if (joint != null)
        {
            Destroy(joint); // Remove joint
            joint = null;
        }
        pullingObjectRB = null;
        isPulling = false;
        animator.SetBool("isPulling", false);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isPulling)
        {
            animator.SetTrigger("jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void MoveCharacter()
    {
        if (isPulling)
        {
            if (pullingObjectRB != null)
            {
                Vector3 directionToBox = (pullingObjectRB.transform.position - transform.position).normalized;
                directionToBox.y = 0f; // Ignore vertical movement

                // Player can only move backward (opposite direction of pulling direction)
                float vertical = Input.GetAxis("Vertical");

                // Only allow backward movement (negative direction)
                if (vertical < 0) // If pressing back (negative)
                {
                    // Move backward along the direction to the box
                    // Calculate the force needed to pull the object
                    // F = ma => F = (mass of object) * (acceleration)
                    float objectMass = pullingObjectRB.mass; // Get the mass of the object
                    float acceleration = moveSpeed / playerMass; // Use the player's mass to calculate acceleration

                    // Newton's Law: Force to move the player backward = mass * acceleration
                    float forceMagnitude = (objectMass * acceleration) ; // Apply a constant to scale force

                    // Apply force to move the player backward
                    Vector3 moveDirection = -directionToBox * Mathf.Abs(vertical); // Move backward
                    rb.AddForce(moveDirection * forceMagnitude, ForceMode.Force);

                    // Update animation state
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    // If the player tries to move forward, do nothing
                    animator.SetBool("isMoving", false);
                }
            }
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    void ApplyGravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f); 

    }
}
