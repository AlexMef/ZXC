using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoinsUI : MonoBehaviour
{
    [SerializeField]
    Text text;

    void Start()
    {
        
    }

    void Update()
    {
        text.text = $"Coins x {FindObjectOfType<MainCharacterController>().GetCoins()}";
    }
}
