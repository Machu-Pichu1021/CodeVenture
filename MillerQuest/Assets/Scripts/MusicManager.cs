using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource source;

    [SerializeField] private AudioClip mainMenuTheme;
    [SerializeField] private AudioClip puzzleTheme;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            ChangeTheme(mainMenuTheme);
        else if (level == 1)
            ChangeTheme(puzzleTheme);
    }

    private void ChangeTheme(AudioClip music)
    {
        source.clip = music;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}
