using System;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] Light light;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            light.color = Color.green;
            LightSwichManager.instance.TurnOnLight(index);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            light.color = Color.red;
            LightSwichManager.instance.TurnOffLight(index);
        }
    }
}
