using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 movementVector;
    private Vector3 goalPosition;
    private float frameCounter = 0;
    
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Collision with player detected!");
            player = collision.gameObject;
            goalPosition = new Vector3(
                collision.gameObject.transform.position.x + 8,
                transform.position.y + 13,
                collision.gameObject.transform.position.z - 2
            );
            movementVector = goalPosition - player.transform.position;
        }
    }

    void Update() {
        if (player && frameCounter < 70) {
            player.transform.position += movementVector * Time.deltaTime;
            frameCounter++;
        } else {
            player = null;
            frameCounter = 0;
        }
    }
}
