using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private bool shouldMove = false;
    public float movementSpeed = 2;
    
    // Start is called before the first frame update
    void Start() {
        shouldMove = false;
    }

    void Update()
    {
        if (shouldMove)
            transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
    }

    public void startMovement() {
        shouldMove = true;
    }

    public void endMovement() {
        shouldMove = false;
    }
}
