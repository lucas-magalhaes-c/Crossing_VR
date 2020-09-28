using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private bool shouldMove = false;
    private bool finished = false;
    public float movementSpeed = 2;
    public AudioSource footstepsAudio;
    public GameObject moveButtonObj;
    public GameObject finishWallObj;
    private Renderer moveButton;
    private Renderer finishWall;
    
    // Start is called before the first frame update
    void Start() {
        shouldMove = false;
        footstepsAudio = GetComponent<AudioSource>();
        moveButton = moveButtonObj.GetComponent<Renderer>();
        finishWall = finishWallObj.GetComponent<Renderer>();
    }

    void Update()
    {
        if (shouldMove)
            transform.position -= Vector3.forward * movementSpeed * Time.deltaTime;
        if (!finished && transform.position.z < finishWallObj.transform.position.z + 5) {
            finished = true;
            finishWall.material.color = Color.green;
        }
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
