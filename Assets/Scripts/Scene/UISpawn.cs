using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UISpawn : MonoBehaviour
{
    public Color newColor = Color.red; // Color al que se cambiará el objeto al colisionar con el jugador
    public float transitionSpeed = 0.5f; // Velocidad de la transición
    private SpriteRenderer spriteRenderer;
    private Color initialColor;

    private void Start()
    {
        // Obtener el componente SpriteRenderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Almacenar el color inicial del objeto
        initialColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que colisionó tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Iniciar la transición del color
            StartCoroutine(ChangeColorSmooth(newColor));
        }
    }

    private System.Collections.IEnumerator ChangeColorSmooth(Color targetColor)
    {
        float elapsedTime = 0f;
        Color currentColor = spriteRenderer.color;

        // Interpolar el color actual al color objetivo
        while (elapsedTime < transitionSpeed)
        {
            spriteRenderer.color = Color.Lerp(currentColor, targetColor, (elapsedTime / transitionSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que el color final sea exactamente el color objetivo
        spriteRenderer.color = targetColor;
    }
}


