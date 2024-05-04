using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Shader _shader;
    public Color _color;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shader = Shader.Find("GUI/Text Shader");
    }

    void ColorSprite()
    {
        _spriteRenderer.material.shader = _shader;
        _spriteRenderer.color = _color;
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        ColorSprite();
    }
}
