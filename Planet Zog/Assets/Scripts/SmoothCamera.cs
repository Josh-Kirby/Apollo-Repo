
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{

    Transform player;
    public float smoothSpeed = .125f;
    public float left;
    public float right;
    public float top;
    public float bottom;
    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Vector2 clamp = new Vector2(Mathf.Clamp(player.position.x, left, right), Mathf.Clamp(player.position.y, bottom, top));
            Vector3 desiredPos = Vector3.Lerp(transform.position, new Vector3(clamp.x, clamp.y, transform.position.z), smoothSpeed);
            transform.position = desiredPos;
        }
    }
}
