using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float collisionSequenceTime;
    [SerializeField] float nextLevelTime;

    Rigidbody   rigBody;
    Mover       mover;

    
    string collidedWith;

    int currentSceneIndex;
    int nextSceneIndex;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody>();
        mover   = GetComponent<Mover>();
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
                Invoke("LoadNextLevel", nextLevelTime);
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
        mover.enabled           = false;
        Invoke("ReloadLevel", collisionSequenceTime);
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
}
