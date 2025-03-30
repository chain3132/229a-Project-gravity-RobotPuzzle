using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
  [Header("Audio sources")]
  [SerializeField] private AudioSource music;
  
  
  [Header("Audio clips")]
  public AudioClip background;
  
  public static Audio instance;
  private void Awake()
  {
      if (instance == null)
      {
          instance = this;
          DontDestroyOnLoad(gameObject);
      }
      else
      {
       Destroy(gameObject);   
      }
  }

  void Start()
  {
      music.clip = background;
      music.Play();
  }
}
