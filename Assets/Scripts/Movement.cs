using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{    
    [SerializeField] float mainThrust = 2000f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;
    [SerializeField] ParticleSystem leftBoostParticles;


    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     	ProcessThrust();  
     	ProcessRotate(); 
    }

    void ProcessThrust()
    {
    	if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotate()
    {
    	if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoostParticles.isEmitting)
        {
            mainBoostParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBoostParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotateThrust);
        if (!leftBoostParticles.isEmitting)
        {
            leftBoostParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotateThrust);
        if (!rightBoostParticles.isEmitting)
        {
            rightBoostParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze
    }

    void StopRotate()
    {
        rightBoostParticles.Stop();
        leftBoostParticles.Stop();
    }
}



