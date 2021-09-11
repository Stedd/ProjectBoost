
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float thrustBase;
    [SerializeField] float thrustModifier;

    [SerializeField] float rotationSpeedBase;
    [SerializeField] float rotationSpeedModifier;

    public float thrustCommand;
    public float rotationCommand;

    public Vector3 thrustFinal;
    public Vector3 rotationSpeedFinal;
    
    Rigidbody rigBody;
    AudioSource audSource;

    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
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
        thrustCommand = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            thrustCommand = 1;
            PlayAudio();
        }
        else
        {
            StopAudio();
        }

        thrustFinal = Vector3.up * thrustCommand * thrustBase * thrustModifier;
        ApplyForce(thrustFinal);
    }
    void ProcessRotation()
    {
        rotationCommand = 0;
        if (Input.GetKey(KeyCode.A))
        {
            rotationCommand = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationCommand = -1;
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

    private void PlayAudio()
    {
        if (!audSource.isPlaying)
        {
            audSource.Play();
        }
    }

    private void StopAudio()
    {
        if (audSource.isPlaying)
        {
            audSource.Stop();
        }
    }

}
