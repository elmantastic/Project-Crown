using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;
    private AudioSource source;

    [Range(0.0f, 1.0f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1.0f;

    [Range(0.0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0.0f, 0.5f)]
    public float randomPitch = 0.1f;
    
    public void SetSource (AudioSource _source){
        source = _source;
        source.clip = clip;
    }

    public void Play(){
        source.volume = volume * (1 + Random.Range(-randomVolume / 2.0f, randomVolume / 2.0f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2.0f, randomPitch / 2.0f));
        source.Play();
    }

}


public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

}
