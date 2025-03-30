using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip[] songs;   
    public void PlaySound(int index)
    {
        AudioSource.PlayClipAtPoint(clips[index], transform.position);
    }
    public static SoundManager instance;
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
}
