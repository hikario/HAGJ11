using UnityEngine;

public class FailSetup : MonoBehaviour
{
    [SerializeField]
    private GameObject audioSource;
    private AudioSource audioS;
    public GameObject starScore;
    private float ogVolume;

    private bool failed;

    public AudioClip failSound;

    public GameObject nextButton;

    private bool failSoundPlayed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextButton.SetActive(false);
        failed = starScore.GetComponent<StarScore>().failed;
        audioS = audioSource.GetComponent<AudioSource>();
        ogVolume = audioS.volume;
        audioS.loop = false;
        if (failed)
        {
            failSoundPlayed = true;
            audioS.clip = failSound;
            audioS.volume = ogVolume;
            audioS.Play();
            //audioSource.GetComponent<AudioFade>().FadeOutMusic();
        }
    }

    //void Update()
    //{
    //    if (audioS.volume <= 0.1)
    //    {
    //        if (!failSoundPlayed)
    //        {
                
    //        }
    //    }
    //}
}
