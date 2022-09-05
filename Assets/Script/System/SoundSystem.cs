using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;

    public static SoundSystem instance;

    public void Awake()
    {
        var soundObject = FindObjectsOfType<SoundSystem>();

        if (soundObject.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void Sound(int number)
    {
        audioSource.PlayOneShot(clip[number]);
    }
}
