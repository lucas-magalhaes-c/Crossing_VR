using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private bool shouldMove = false;
    private bool finished = false;
    public float movementSpeed = 2;
    public float moveButtonHeightDistance = 0.8f;
    public AudioSource footstepsAudio;
    public GameObject moveButtonObj;
    public GameObject finishWallObj;
    private Camera mainCamera;
    private Renderer moveButton;
    private Renderer finishWall;
    
    // Start is called before the first frame update
    void Start() {
        shouldMove = false;
        mainCamera = Camera.main;
        footstepsAudio = GetComponent<AudioSource>();
        moveButton = moveButtonObj.GetComponent<Renderer>();
        finishWall = finishWallObj.GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3 newPosition;
        moveButton.transform.position = new Vector3(moveButton.transform.position.x, mainCamera.transform.position.y + moveButtonHeightDistance, moveButton.transform.position.z);       
        if (shouldMove) {
            newPosition = mainCamera.transform.TransformDirection(new Vector3(
                0,
                0,
                movementSpeed * Time.deltaTime));
            transform.position += new Vector3(newPosition.x, 0, newPosition.z);
        }
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
