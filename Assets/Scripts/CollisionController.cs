using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField]
    GameObject gameObj;
    int enemyLayer = 8;
    int deathzoneLayer = 9;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            FindObjectOfType<DamageController>().ReceiveDamage();
        }

        if (collision.gameObject.layer == deathzoneLayer)
        {
            FindObjectOfType<DamageController>().ReceiveDamage();
        }
    }
}
