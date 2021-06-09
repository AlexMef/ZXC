using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObj;
    private MainCharacterController mainController;
    private string tagDoubleJump = "PowerupDoubleJump";
    private string tagSpeed = "PowerupSpeed";
    private string tagJump = "PowerupJump";
    private const int _defaultPowerupDuration = 5;
    // Start is called before the first frame update
    void Start()
    {
        mainController = _gameObj.GetComponent<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collect coins
        if (collision.gameObject.layer == mainController.coinLayer)
        {
            mainController.AddCoins();
            Destroy(collision.gameObject);
        }
        // select powerup by tag
        if (collision.gameObject.layer.Equals(mainController.powerupLayer))
        {
            if (collision.gameObject.CompareTag(tagJump))
            {
                _ = UsePowerup(tagJump, 10);
                Destroy(collision.gameObject);
            }
            
            if (collision.gameObject.CompareTag(tagSpeed))
            {
                _ = UsePowerup(tagSpeed, 10);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag(tagDoubleJump))
            {
                _ = UsePowerup(tagDoubleJump, 0);
                Destroy(collision.gameObject);
            }


        }
    }



    public void RestoreCollectables()
    {
        
    }

    public async Task Timer(int seconds)
    {
        DateTime timeNow = System.DateTime.Now;
        print(timeNow.ToString());
        DateTime timeWait = System.DateTime.Now.AddSeconds(seconds);
        while (timeNow <= timeWait)
        {
            timeNow = System.DateTime.Now;
            await Task.Yield();
        }
    }

    public async Task UsePowerup(string tag, int powerUpDuration = _defaultPowerupDuration)
    {
        if (mainController.powerupEnabled)
        {
            print("Powerup is already applied");
        }
        if (!mainController.powerupEnabled)
        {
            if (tag.Equals(tagSpeed))
            {
                mainController.HorizontalAcceleration = mainController.HorizontalAcceleration *= 2;
                mainController.EnablePowerup();
                await Timer(powerUpDuration);
                mainController.HorizontalAcceleration = mainController.HorizontalAcceleration /= 2;
                mainController.DisablePowerup();
            }
            if (tag.Equals(tagJump))
            {
                mainController.VerticalAcceleration = mainController.VerticalAcceleration *= 2;
                await Timer(powerUpDuration);
                mainController.VerticalAcceleration = mainController.VerticalAcceleration /= 2;
            }
        }
        // apply constant powerups
        if (tag == tagDoubleJump)
        {
            if (!mainController.AbilityDoubleJump) mainController.AbilityDoubleJump = true;
        }



    }


}
