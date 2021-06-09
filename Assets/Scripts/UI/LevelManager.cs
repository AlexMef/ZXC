using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Canvas overlayMenu;
    [SerializeField]
    private Button resumeBtn, restartBtn, exitBtn;

    void Start()
    {
        overlayMenu = gameObject.GetComponent<Canvas>();
        resumeBtn.onClick.AddListener(OnResume);
        restartBtn.onClick.AddListener(OnRestart);
        exitBtn.onClick.AddListener(OnExit);
    }

    // Update is called once per frame
    void Update()
    {
        bool menuCalled = Input.GetKeyUp(KeyCode.Escape);
        if (menuCalled)
        {
            if (overlayMenu.enabled)
            {
                overlayMenu.enabled = false;
            } 
            else
            {
                overlayMenu.enabled = true;
            }
            
        }
    }

    // todo: implement UI overlay
    private void OnResume()
    {
        overlayMenu.enabled = false;
    }

    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    private void OnExit()
    {
        SceneManager.LoadScene("MainMenuGUI");
    }

}
