using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Handles UI interactions in main manu (out-game)
/// </summary>
public class OnClickMainMenu : MonoBehaviour
{

    [SerializeField]
    Button startBtn, loadBtn, exitBtn;
    void Start()
    {
        startBtn.onClick.AddListener(onClickStartBtn);
        exitBtn.onClick.AddListener(onClickExitBtn);
    }

    private void onClickStartBtn()
    {
        SceneManager.LoadScene("Level1");
    }

    private void onClickLoadBtn()
    {
        print("load save");
    }

    private void onClickExitBtn()
    {
        Application.Quit();
    }

}
