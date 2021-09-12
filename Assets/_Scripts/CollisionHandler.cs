using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float collisionSequenceTime;
    [SerializeField] float nextLevelTime;

    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    Rigidbody   rigBody;
    Mover       mover;
    AudioSource audSource;

    string collidedWith;

    int currentSceneIndex;
    int nextSceneIndex;

    bool isTransitioning;

    private void Start()
    {
        rigBody     = GetComponent<Rigidbody>();
        mover       = GetComponent<Mover>();
        audSource   = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        collidedWith = collision.gameObject.tag;

        if (isTransitioning) { return; }

        switch (collidedWith)
        {
            case "Friendly":
                Debug.Log("On the launchpad!");
                break;
            case "Finish":
                StartNextLevelSequence();
               
                break;
            default:
                StartCollisionSequence();
                break;
        }
    }

    void StartCollisionSequence()
    {
        rigBody.freezeRotation  = false;
        rigBody.constraints     = 0;
        mover.enabled = false;

        audSource.Stop();

        crashParticles.Play();

        PlayAudio(crashSound);
     
        Invoke("ReloadLevel", collisionSequenceTime);

        isTransitioning = true;
    }

    void StartNextLevelSequence()
    {
        mover.enabled = false;

        audSource.Stop();

        successParticles.Play();

        PlayAudio(successSound);
        
        Invoke("LoadNextLevel", nextLevelTime);
        
        isTransitioning = true;
    }

    void LoadNextLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void PlayAudio(AudioClip _clip)
    {
        audSource.PlayOneShot(_clip);
    }

    //private void StopAudio()
    //{
    //    if (audSource.isPlaying)
    //    {
    //        audSource.Stop();
    //    }
    //}


}
