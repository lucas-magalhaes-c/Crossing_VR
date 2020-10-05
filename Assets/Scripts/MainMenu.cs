using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    
    
    // Start is called before the first frame update
    public void StartGame()
    {
        HideMenu();
        // scenesToLoad.Add(SceneManager.LoadSceneAsync("Crevasse"));
    }

    // Update is called once per frame
    public void HideMenu()
    {
        
    }
}
