using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource source;
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float rotationForce = 1f;
    [SerializeField] AudioClip thrustClip;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        Debug.Log("Thrusting");
        CheckIfSpacePressed();
    }

    void CheckIfSpacePressed()
    {
        if (Input.GetKey(KeyCode.Space) ||
            Input.GetKey("up") ||
            Input.GetKey(KeyCode.W))
        {
            SpacePressed();
        }
        else { source.Stop(); }
    }

    void SpacePressed()
    {
        Debug.Log("Space Has Been Pressed");
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (source.isPlaying != true)
        {
            source.PlayOneShot(thrustClip);
        }
    }

    void ProcessRotation()
    {
        Debug.Log("Rotating");
        void ApplyRotation(float rotateforce)
        {
            FreezeRotation(rotateforce);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationForce);
        }
    }

    void FreezeRotation(float rotateforce)
    {
        rb.freezeRotation = true; // freezing rotation 
        transform.Rotate(Vector3.forward * rotateforce * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation
    }
}
