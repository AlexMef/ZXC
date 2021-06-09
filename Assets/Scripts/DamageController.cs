using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageController : MonoBehaviour
{
    // define from which script to reference
    private MainCharacterController mainCharacterController;
    void Start()
    {
        mainCharacterController = FindObjectOfType<MainCharacterController>();
    }

    public void ReceiveDamage()
    {
        if (mainCharacterController.Health > 0)
        {
            mainCharacterController.Respawn();
            mainCharacterController.Health--;
        }
        if (mainCharacterController.Health == 0)
        {
            // game over
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
