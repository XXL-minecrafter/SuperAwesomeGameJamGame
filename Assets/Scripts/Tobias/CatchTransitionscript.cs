using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchTransitionscript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Image blackscreen;
    [SerializeField] bool blackout;
    [SerializeField] float secondsInBlackScreen;


    [SerializeField, Range(0.1f, 1)] float animationspeed;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        StartCoroutine(StartTransition());
    }
    public IEnumerator StartTransition()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * animationspeed;
            yield return null;
        }
        yield return new WaitForSeconds(secondsInBlackScreen);

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * animationspeed;
            yield return null;

        }
    }

}
