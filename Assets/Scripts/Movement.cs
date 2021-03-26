using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 2000f;
    [SerializeField] float rotateThrust = 100f;

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
    		rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
    	}
        else 
        {
            audioSource.Stop();
        }
    }

    void ProcessRotate()
    {
    	if (Input.GetKey(KeyCode.D))
    	{
    		ApplyRotation(rotateThrust);
    	}

    	else if (Input.GetKey(KeyCode.A))
    	{
    		ApplyRotation(-rotateThrust);
    	}
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze
    }
}



