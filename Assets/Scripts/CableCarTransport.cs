using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCarTransport : MonoBehaviour
{
    public float speed = 1f;
    public GameObject goalObject;
    private GameObject player = null;
    private Vector3 movementVector;
    private Vector3 goalPosition;
    private float frameCounter = 0;
    private MovePlayer movePlayer;
    
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Collision with player detected!");
            player = collision.gameObject;
            movePlayer = player.GetComponent<MovePlayer>();
            goalPosition = goalObject.transform.position;
            movementVector = goalPosition - player.transform.position;
            movePlayer.stopPlayerMovement();
            movePlayer.disablePlayerGravity();
        }
    }

    void Update() {
        if (player) {
            player.transform.position += movementVector * Time.deltaTime * speed;
            frameCounter++;
        } else {
            player = null;
            frameCounter = 0;
        }
    }
}
