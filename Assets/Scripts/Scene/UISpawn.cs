using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UISpawn : MonoBehaviour
{
    public Color newColor = Color.red; 
    public float transitionSpeed = 0.5f;
    private SpriteRenderer spriteRenderer;
    private Color initialColor;

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        initialColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            StartCoroutine(ChangeColorSmooth(newColor));
        }
    }

    private System.Collections.IEnumerator ChangeColorSmooth(Color targetColor)
    {
        float elapsedTime = 0f;
        Color currentColor = spriteRenderer.color;

        
        while (elapsedTime < transitionSpeed)
        {
            spriteRenderer.color = Color.Lerp(currentColor, targetColor, (elapsedTime / transitionSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        spriteRenderer.color = targetColor;
    }
}


