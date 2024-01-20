using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Hemos intentado poner un texto encima de la fogata al entrar pero no ha salido muy bien, lo dejamos para cuando
//estemos en clase con el Victor
public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bonfireText;
    [SerializeField] private float _offset;

    public void ShowText(Transform position)
    {
        _bonfireText.transform.position = new Vector3(position.position.x, position.position.y + _offset, 0);
        _bonfireText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        _bonfireText.gameObject.SetActive(false);
    }
}
