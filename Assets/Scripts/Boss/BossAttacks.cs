using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject _damageBall;
    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private Transform _krokurPhaseAttackPosition;
    [SerializeField] private float _krokurAttackDelay;
    private bool canMoveToPosition = false;
    [SerializeField] private bool _attackStarted = false;

    public void Attack()
    {
        switch (GameManager.Instance._currentCharacter)
        {
            case GameManager.Character.AIKE:
                AikePhase();
                break;
            case GameManager.Character.KROKUR:
                _attackStarted = false;
                canMoveToPosition = true;
                break;
        }
        
    }

    private void KrokurPhase()
    {
        StartCoroutine(KrokurPhaseAttack());
    }

    private IEnumerator KrokurPhaseAttack()
    {
        _attackStarted = true;
        gameObject.GetComponent<BossMovement>().enabled = false;
        for(int i = 0; i < 10; i++)
        {
            GameObject tempO = Instantiate(_damageBall, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
            yield return new WaitForSeconds(_krokurAttackDelay);
        }
        gameObject.GetComponent<BossMovement>().enabled = true;
    }

    private void Update()
    {
        if (canMoveToPosition)
        {
            gameObject.GetComponent<BossMovement>().MoveToPosition(_krokurPhaseAttackPosition);
        }

        if(Vector3.Distance(transform.position, _krokurPhaseAttackPosition.position) < 0.1f)
        {
            transform.position = _krokurPhaseAttackPosition.position;
            canMoveToPosition = false;
        }

        if(Vector3.Distance(transform.position, _krokurPhaseAttackPosition.position) < 0.1f)
        {
            

        }
    }

    private void AikePhase()
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
