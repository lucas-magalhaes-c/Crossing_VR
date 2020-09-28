using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private bool shouldMove = false;
    public float movementSpeed = 2;
    public AudioSource footstepsAudio;
    public GameObject moveButtonObj;
    private Renderer moveButton;
    
    // Start is called before the first frame update
    void Start() {
        shouldMove = false;
        footstepsAudio = GetComponent<AudioSource>();
        moveButton = moveButtonObj.GetComponent<Renderer>();
    }

    void Update()
    {
        if (shouldMove)
            transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
    }

    public void startMovement() {
        shouldMove = true;
        footstepsAudio.Play(0);
        moveButton.material.color = Color.red;
    }

    public void endMovement() {
        shouldMove = false;
        footstepsAudio.Pause();
        moveButton.material.color = Color.blue;
    }
}
