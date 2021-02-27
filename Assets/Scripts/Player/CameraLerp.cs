using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    private Transform cameraTransform;

    [SerializeField]
    private Transform playerTransform;


    // catchup speed to the player
    [SerializeField]
    private float movementSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newPosition = Vector2.Lerp(cameraTransform.position, playerTransform.position, movementSpeed);

        newPosition.z = -10;

        cameraTransform.position = newPosition;
    }
}
