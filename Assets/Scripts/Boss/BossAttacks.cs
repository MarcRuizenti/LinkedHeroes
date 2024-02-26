using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject _damageBall;
    [SerializeField] private GameObject _parryBall;
    [SerializeField] private List<GameObject> _balls;

    public void Attack()
    {
        for (int i = 0; i < 360; i += 45)
        {
            GameObject tempO = Instantiate(_damageBall, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);

            tempO.transform.localEulerAngles = new Vector3(tempO.transform.rotation.x, tempO.transform.rotation.y, i);

            _balls.Add(tempO);
        }

        int rand1 = Random.Range(0, 5);
        int rand2 = Random.Range(5, 8);
        _balls[rand1].GetComponent<SpriteRenderer>().color = Color.blue;
        _balls[rand2].GetComponent<SpriteRenderer>().color = Color.blue;
        _balls[rand1].tag = "Parry";
        _balls[rand2].tag = "Parry";

        _balls.Clear();
    }
}
