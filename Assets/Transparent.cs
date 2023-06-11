using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour, ITransparent
{
    private Renderer rendererRoof;
    private float startTime;
    [SerializeField] float fadeDuration = 0.5f;
    private bool isTransparent = false;
    public bool IsTransparent { get => isTransparent; set => isTransparent = value;}
    void Start()
    {
        isTransparent = false;
        rendererRoof = GetComponent<Renderer>();
        Color currentColor = rendererRoof.material.color;
    }


    public IEnumerator TransparentOn()
    {
        IsTransparent = true;

        startTime = Time.time;
        while (Time.time - startTime < fadeDuration)
        {
            
            float elapsed = Time.time - startTime;
            float alpha = Mathf.Lerp(255, 0, elapsed / fadeDuration);

            Color newColor = rendererRoof.material.color;
            newColor = new Color(newColor.r, newColor.g, newColor.b, alpha);
            rendererRoof.material.color = newColor;

            yield return null;
        }
        
    }
    public IEnumerator TransparentOff()
    {
        IsTransparent = false;

        startTime = Time.time;
        while (Time.time - startTime < fadeDuration)
        {
            float elapsed = Time.time - startTime;
            float alpha = Mathf.Lerp(0, 255, elapsed / fadeDuration);

            Color newColor = rendererRoof.material.color;
            newColor = new Color(newColor.r, newColor.g, newColor.b, alpha);
            rendererRoof.material.color = newColor;

            yield return null;
        }
    }

}
