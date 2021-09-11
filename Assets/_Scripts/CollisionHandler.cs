using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float collisionSequenceTime;
    [SerializeField] float nextLevelTime;

    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    Rigidbody   rigBody;
    Mover       mover;
    AudioSource audSource;


    string collidedWith;

    int currentSceneIndex;
    int nextSceneIndex;

    private void Start()
    {
        rigBody     = GetComponent<Rigidbody>();
        mover       = GetComponent<Mover>();
        audSource   = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        collidedWith = collision.gameObject.tag;

        switch (collidedWith)
        {
            case "Friendly":
                Debug.Log("On the launchpad!");
                break;
            case "Finish":
                NextLevel();
                break;
            default:
                Collided();
                break;
        }
    }

    void Collided()
    {
        rigBody.freezeRotation  = false;
        rigBody.constraints     = 0;
        mover.enabled = false;

        PlayAudio(crashSound);
     
        Invoke("ReloadLevel", collisionSequenceTime);
    }

    void NextLevel()
    {
        mover.enabled = false;
        
        PlayAudio(successSound);
        
        Invoke("LoadNextLevel", nextLevelTime);
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
