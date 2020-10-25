using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void playScene() {
        Debug.Log("Clicked");
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
