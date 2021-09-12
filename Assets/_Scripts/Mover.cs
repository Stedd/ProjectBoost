
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float thrustBase;
    [SerializeField] float thrustModifier;

    [SerializeField] float rotationSpeedBase;
    [SerializeField] float rotationSpeedModifier;

    [SerializeField] AudioClip mainEngineSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;



    public float thrustCommand;
    public float rotationCommand;

    public Vector3 thrustFinal;
    public Vector3 rotationSpeedFinal;
    
    Rigidbody rigBody;
    AudioSource audSource;

    // Start is called before the first frame update
    void Start()
    {
        rigBody     = GetComponent<Rigidbody>();
        audSource   = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        //Wind();
    }

    void Wind()
    {
        ApplyForce(Vector3.left*50000);
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            thrustCommand = 1;
            PlayAudio(mainEngineSound);
            PlayParticles(mainEngineParticles);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            thrustCommand = 0;
            StopAudio();
            StopPatricles(mainEngineParticles);
        }

        thrustFinal = Vector3.up * thrustCommand * thrustBase * thrustModifier;
        ApplyForce(thrustFinal);

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotationCommand = 1;
            PlayParticles(rightThrusterParticles);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationCommand = -1;
            PlayParticles(leftThrusterParticles);
        }
        else
        {
            rotationCommand = 0;
            StopPatricles(rightThrusterParticles);
            StopPatricles(leftThrusterParticles);
        }

        rotationSpeedFinal = Vector3.forward * rotationCommand * rotationSpeedBase * rotationSpeedModifier;
        ApplyTorque(rotationSpeedFinal);
    }

    public void ApplyForce(Vector3 _vec){
        rigBody.AddRelativeForce(_vec * Time.deltaTime);
    }

    public void ApplyTorque(Vector3 _vec)
    {
        rigBody.AddRelativeTorque(_vec * Time.deltaTime);
    }

    void PlayAudio(AudioClip _clip)
    {
        if (!audSource.isPlaying)
        {
            audSource.PlayOneShot(_clip);
        }
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
