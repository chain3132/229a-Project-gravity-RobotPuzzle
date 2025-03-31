using System;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] EnemyFOV enemyFOV;
    [SerializeField]Animator anim;
    public bool isDead = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Attack"))
        {
            Debug.Log("Player died");
            anim.SetTrigger("Dead");
            enemyFOV.StopAllCoroutines();
            isDead = true;
        }
        
    }
    
}
