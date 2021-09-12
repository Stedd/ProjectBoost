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
        FreeConstraints();
        
        DisableMover();

        StopAudio();

        PlayParticles(crashParticles);

        PlayAudioOneShot(crashSound);

        Invoke("ReloadLevel", collisionSequenceTime);

        isTransitioning = true;
    }


    void StartNextLevelSequence()
    {
        DisableMover();

        StopAudio();

        PlayParticles(successParticles);

        PlayAudioOneShot(successSound);
        
        Invoke("LoadNextLevel", nextLevelTime);
        
        isTransitioning = true;
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(GetNextSceneIndex());
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(GetCurrentSceneIndex());
    }

    private int GetNextSceneIndex()
    {
        int _nextSceneIndex = GetCurrentSceneIndex() + 1;

        if (_nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            _nextSceneIndex = 0;
        }
        return _nextSceneIndex;
    }
    private int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void DisableMover()
    {
        mover.enabled = false;
    }

    private void FreeConstraints()
    {
        rigBody.freezeRotation = false;
        rigBody.constraints = 0;
    }
    private void PlayAudioOneShot(AudioClip _clip)
    {
        audSource.PlayOneShot(_clip);
    }

    private void StopAudio()
    {
        if (audSource.isPlaying)
        {
            audSource.Stop();
        }
    }
    void PlayParticles(ParticleSystem _particles)
    {
        if (!_particles.isPlaying)
        {
            _particles.Play();
        }
    }
    void StopPatricles(ParticleSystem _particles)
    {
        _particles.Stop();
    }
}
