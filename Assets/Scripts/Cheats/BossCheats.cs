using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheats : MonoBehaviour
{
    [SerializeField] private BossController _boss;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            _boss.TakeDamage(_boss.healthShield);
        }
    }
}
