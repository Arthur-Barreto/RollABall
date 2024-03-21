using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip background;
    public AudioClip victory;
    public AudioClip gameOver;
    public AudioClip rollFx;
    
    // Start is called before the first frame update
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
