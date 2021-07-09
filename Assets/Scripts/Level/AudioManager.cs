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

    public void Stop(){
        source.Stop();
    }

}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] Sound[] sounds;

    private void Awake() {
        if(instance != null){
            Debug.LogError("More than one AudioManager in the scene");
        } else {
            instance = this;
        }
        // if(instance == null){
        //     instance = this;
        //     return;
        // }

    }

    private void Start() {
        for (int i = 0; i < sounds.Length; i++){
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name){
        for (int i = 0; i < sounds.Length; i++){
            if(sounds[i].name == _name){
                sounds[i].Play();
                return;
            }
        }

        // No Sound Found With _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }
    public void StopSound(string _name){
        for (int i = 0; i < sounds.Length; i++){
            if(sounds[i].name == _name){
                sounds[i].Stop();
                return;
            }
        }

        // No Sound Found With _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

}
