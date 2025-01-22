using System.Collections;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private float scrollSpeed = 1f;
    private Material myMaterial;
    private Coroutine coroutine = null;

    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        
        EventBus.Subscribe(EventType.COUNTDOWN, GoFoward);
        EventBus.Subscribe(EventType.END, GameOver);
    }

    private void GoFoward()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(Scroll());
    }
    
    private IEnumerator Scroll()
    {
        while(true)
        {
            float newOffsetY = myMaterial.mainTextureOffset.y + Time.deltaTime * scrollSpeed;

            Vector2 newOffSet = new Vector2(0, newOffsetY);
            
            myMaterial.mainTextureOffset = newOffSet;

            yield return null;
        }
    }

    private void GameOver()
    {
        StopCoroutine(coroutine);
    }
}