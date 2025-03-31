using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwichScene : MonoBehaviour
{   
    [SerializeField] private int index;
    [SerializeField] private Die isEnemyDead;
    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            
                if (index == 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (index == 3)
                {
                    if (isEnemyDead.isDead)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    }
                }
                else if (LightSwichManager.instance.count == 4 && index ==2)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
        }
    }

    
}
