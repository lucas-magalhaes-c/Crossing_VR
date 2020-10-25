using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    public bool loadNextScene = false;
    public string nextScene = "Crevasse";
    public float movementSpeed = 2;
    public float moveButtonHeightDistance = 0.8f;
    public float cameraDefaultFOV = 60.0f;
    public float cameraMaxFOV = 90.0f;
    public float dollyEffectTiltInitPoint = 0f;
    public float dollyEffectTiltEndPoint = -1.0f;
    public float dollyEffectWiggleMaxVar = 5.0f;
    public float dollyEffectWiggleSpeed = 1f;
    public bool dollyEffectEnableWiggle = true;
    public bool dollyEffectWiggleRandomVar = false;
    public AudioSource footstepsAudio;
    public GameObject moveButtonObj;
    public GameObject finishWallObj;
    public GameObject deathZone;

    private Camera mainCamera;
    private Renderer moveButton;
    private Renderer finishWall;
    private Rigidbody playerBody;
    private bool shouldMove = false;
    private float wiggleProgress = 0f;
    private float wiggleDirection = 1f;
    
    // Start is called before the first frame update
    void Start() {
        shouldMove = false;
        mainCamera = Camera.main;
        footstepsAudio = GetComponent<AudioSource>();
        playerBody = GetComponent<Rigidbody>();
        moveButton = moveButtonObj.GetComponent<Renderer>();
        finishWall = finishWallObj.GetComponent<Renderer>();
        playerBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        playerBody.freezeRotation = true;
        dollyEffect();
        movePlayer();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == finishWallObj) {
            Debug.Log("Collision with finish wall detected!");
            finishWall.material.color = Color.green;
            if (loadNextScene)
                SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        } else if (collision.gameObject == deathZone) {
            Debug.Log("Collision with death zone detected!");
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        } else if (collision.gameObject.tag == "Transporter") {
            Debug.Log("Collision with transporter detected!");
            endMovement();
        } else
            Debug.Log("Collision detected!");
    }

    public void startMovement() {
        shouldMove = true;
        footstepsAudio.Play(0);
        moveButton.material.color = Color.red;
    }

    public void endMovement() {
        Debug.Log("End movement");
        shouldMove = false;
        footstepsAudio.Pause();
        moveButton.material.color = Color.blue;
    }

    private void movePlayer() {
        if (!shouldMove) return;
        
        Vector3 newPosition;
        newPosition = mainCamera.transform.TransformDirection(new Vector3(
            0,
            0,
            movementSpeed * Time.deltaTime));
        transform.position += new Vector3(newPosition.x, 0, newPosition.z);
    }

    private void dollyEffect() {
        float cameraYTilt = mainCamera.transform.forward[1];
        moveButton.transform.position = new Vector3(moveButton.transform.position.x, mainCamera.transform.position.y + moveButtonHeightDistance, moveButton.transform.position.z);
        float multFactor = ((cameraMaxFOV - cameraDefaultFOV) / (dollyEffectTiltEndPoint - dollyEffectTiltInitPoint));
        float constFactor = cameraMaxFOV - multFactor * dollyEffectTiltEndPoint;
        float newFOVCalculator = constFactor + multFactor * cameraYTilt;
        mainCamera.fieldOfView = Mathf.Max(Mathf.Min(newFOVCalculator, cameraMaxFOV) + dollyEffectWiggle(), cameraDefaultFOV);
        if (mainCamera.fieldOfView > cameraDefaultFOV) {
            mainCamera.fieldOfView += dollyEffectWiggleMaxVar * Time.deltaTime;
        }
    }

    private float dollyEffectWiggle() {
        if (!dollyEffectEnableWiggle) return 0f;

        wiggleProgress += dollyEffectWiggleMaxVar * Time.deltaTime * wiggleDirection * dollyEffectWiggleSpeed;
        if (Mathf.Abs(wiggleProgress) > dollyEffectWiggleMaxVar) {
            wiggleDirection *= -1f * (dollyEffectWiggleRandomVar ? Random.value : 1f);
        }
        return wiggleProgress;
    }
}
