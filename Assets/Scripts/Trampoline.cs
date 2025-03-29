using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceCoefficient = 0.8f;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 velocity = rb.linearVelocity;
            
            if (velocity.y < 0) // 
            {
                float bounceVelocity = Mathf.Abs(velocity.y )* rb.mass * bounceCoefficient  ; // คำนวณความเร็วเด้งกลับ
                rb.linearVelocity = new Vector3(velocity.x, bounceVelocity, velocity.z); // ใช้ค่า velocity ตามกฎของนิวตัน
            }
        }
    }
}
