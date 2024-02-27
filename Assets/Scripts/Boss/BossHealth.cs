using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private Transform _fallPosition;
    private bool _courutineStarted = false;

    public int maxHealth;
    public int health;

    public int maxHealthShield;
    public int healthShield;



    void Start()
    {
        health = maxHealth;
        healthShield = maxHealthShield;
    }


    void Update()
    {
        if (healthShield <= 0)
        {
            gameObject.GetComponent<BossController>().enabled = false;
            float distance = Vector3.Distance(transform.position, _fallPosition.position);
            if (distance > 0.1f && !_courutineStarted) gameObject.GetComponent<BossController>().MoveToPosition(_fallPosition);
            if (!_courutineStarted && distance < 0.1f) StartCoroutine(BossFall());

        }
    }


    public void TakeDamage()
    {
        Debug.Log("Au");
        if (healthShield > 0)
        {
            healthShield--;
        }
        else
        {
            health--;
            healthShield = maxHealthShield;
            gameObject.GetComponent<BossController>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            _courutineStarted = false;
        }
    }

    IEnumerator BossFall()
    {
        _courutineStarted = true;
        Debug.Log("Caigo");

        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBoss"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }


}
