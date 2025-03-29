using System;
using UnityEngine;

public class LightSwichManager : MonoBehaviour
{
    [SerializeField] private Light[] light;
    public int count = 0;
    public static LightSwichManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    
    public void TurnOnLight(int lightIndex)
    {
        light[lightIndex].color = Color.green;
        count++;
    }
    public void TurnOffLight(int lightIndex)
    {
        light[lightIndex].color = Color.red;
        count--;
    }
    
}
