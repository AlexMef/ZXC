using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main character stats, buffs, and default behaviour
/// </summary>
public class MainCharacterController : MonoBehaviour
{
    // coordinates for respawn
    [SerializeField]
    private Vector3 respawnCoord;
    [SerializeField]
    private bool enableDebug = true;
    [SerializeField]
    private float horizontalAcceleration = 7f;
    [SerializeField]
    private float verticalAcceleration = 15f;
    [SerializeField]
    private int coins = 0;
    [SerializeField]
    private bool doubleJumpAbility = false;
    [SerializeField]
    private float shiftAcceleration = 2f;

    //// todo: move to another script
    private AudioSource audioSource = null;
    private List<AudioClip> audioClips = null;
    private Rigidbody2D rb2d = null;
    ////
    private bool isGrounded;
    public bool canDoubleJump = false;
    public readonly int powerupLayer = 6;
    public readonly int coinLayer = 7;
    public readonly int constantPowerupLayer = 8;
    public bool powerupEnabled = false;
    public int healthPoints;

    public void EnablePowerup() { powerupEnabled = true; }
    public void DisablePowerup() { powerupEnabled = false; }


    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public float HorizontalAcceleration { get { return horizontalAcceleration; } set { horizontalAcceleration = value; } }
    public float VerticalAcceleration { get { return verticalAcceleration; } set { verticalAcceleration = value; } }
    public bool AbilityDoubleJump { get { return doubleJumpAbility; } set { doubleJumpAbility = value; } }
    public float ShiftMagnitude { get { return shiftAcceleration; } internal set { shiftAcceleration = value; } }
    public int Health { get { return healthPoints; } set { healthPoints = value; } }

    public int GetCoins()
    {
        return coins;
    }
    public void AddCoins()
    {
        coins++;
        while (coins >= 10)
        {
            HealthUp();
            coins -= 10;
        }
    }
    public void AddCoins(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException($"{ nameof(value)} must be greater than 0");
        coins += value;
        while (coins >= 10)
        {
            HealthUp();
            coins -= 10;
        }
    }

    public void ClearCoins()
    {
        coins = 0;
    }

    public void HealthUp()
    {
        healthPoints++;
    }






    
    private void Start()
    {
        Debug.unityLogger.logEnabled = enableDebug;
        //EnableAudio();

        respawnCoord = gameObject.transform.position;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        // disables body rotation
        rb2d.freezeRotation = true;
        // default health points
        healthPoints = 5;
        // implement health according to hard level
    }

    private void Update()
    {
    }

    /// <summary>
    /// Respawns player
    /// </summary>
    public void Respawn()
    {
        // move player to default position on respawn
        gameObject.transform.position = respawnCoord;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnableAudio()
    {
        //attaches some audio clips as jump sounds
        audioSource = GetComponent<AudioSource>();
        audioClips = new List<AudioClip>
        {
            Resources.Load("Jump_1") as AudioClip,
            Resources.Load("Jump_2") as AudioClip,
            Resources.Load("Jump_3") as AudioClip,
            Resources.Load("Jump_4") as AudioClip,
        };
    }


}
