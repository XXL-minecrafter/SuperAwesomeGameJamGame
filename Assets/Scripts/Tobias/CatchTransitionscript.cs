using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CatchTransitionscript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Image blackscreen;
    [SerializeField] bool blackout;
    [SerializeField] float secondsInBlackScreen;


    public static Action OnFullBlackScreen;

    [SerializeField, Range(0.1f, 1)] float animationspeed;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        PlayerCaught.OnPlayerCaught += Transition;
    }
    private void OnDisable()
    {
        PlayerCaught.OnPlayerCaught -= Transition;

    }
    public void Transition()
    {
        StartCoroutine(StartTransition());
    }
    private IEnumerator StartTransition()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * animationspeed;
            yield return null;
        }
        OnFullBlackScreen?.Invoke();
        yield return new WaitForSeconds(secondsInBlackScreen);

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * animationspeed;
            yield return null;
        }
    }
}
