using UnityEngine;

public class TintController : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    private Color targetColor;
    private Color currentColor;
    private float fadeSpeed = 5f;
    private Material mat;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material = new Material(rend.material); // Ensure it's unique

        if (rend != null)
        {
            // Important: Make sure to use a unique material instance
            rend.material = new Material(rend.material);
            originalColor = rend.material.color;
            currentColor = originalColor;
            targetColor = originalColor;
        }
    }

    public void FadeStart(Color color)
    {
        targetColor = color;
        Timer = 0;
    }

    public void FadeEnd()
    {
        targetColor = originalColor;
        Timer = 0;
    }

    float Timer;
    void Update()
    {
        if (rend != null)
        {
            Timer += Time.deltaTime / fadeSpeed;
            currentColor = Color.Lerp(currentColor, targetColor*0.5f + originalColor*0.5f, Timer);
            rend.material.color = currentColor;
        }
    }
}
