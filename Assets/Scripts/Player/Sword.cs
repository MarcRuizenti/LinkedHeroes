using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Parry":
                Debug.Log("Parry");
                break;
            case "Damage":
                Debug.Log("Damage");
                break;

        }
    }
}