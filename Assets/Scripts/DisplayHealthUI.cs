using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealthUI : MonoBehaviour
{
    [SerializeField]
    private Text text;
    MainCharacterController mainCharacterController;
    void Start()
    {
        mainCharacterController = FindObjectOfType<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Health: {mainCharacterController.Health.ToString()}";
    }
}
