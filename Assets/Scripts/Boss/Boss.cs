using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _damageBall;
    [SerializeField] private GameObject _parryBall;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackCounter;
    [SerializeField] private List<GameObject> _balls;

    void Update()
    {
        if (_attackCounter <= 0)
        {
            Debug.Log("dispara");

            for (int i = 0; i < 360; i += 45)
            {
                GameObject tempO = Instantiate(_damageBall, transform.position, transform.rotation);

                tempO.transform.localEulerAngles = new Vector3(tempO.transform.rotation.x, tempO.transform.rotation.y, i);

                _balls.Add(tempO);
            }

            int rand1 = Random.Range(0, 5);
            int rand2 = Random.Range(5, 8);
            Debug.Log(rand2);
            Debug.Log(rand1);
            _balls[rand1].GetComponent<SpriteRenderer>().color = Color.blue;
            _balls[rand2].GetComponent<SpriteRenderer>().color = Color.blue;

            _attackCounter = _attackDelay;
        }
        else if (_attackCounter > 0) 
        {
            _attackCounter -= Time.deltaTime;

            _balls.Clear();
        }
    }
}
