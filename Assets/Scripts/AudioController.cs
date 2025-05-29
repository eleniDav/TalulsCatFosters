using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource[] soundEffects;

    //the volumes that will change with the slidders
    private float musicVolume = 0.3f;
    private float soundEffectsVolume = 0.6f;

    private void Update()
    {
        bgMusic.volume = musicVolume;
        foreach(AudioSource soundEffect in soundEffects)
        {
            soundEffect.volume = soundEffectsVolume;
        }
    }

    public void ChangeMusicVolume(float m)
    {
        musicVolume = m;
    }

    public void ChangeSoundEffectsVolume(float s)
    {
        soundEffectsVolume = s;
    }
}
