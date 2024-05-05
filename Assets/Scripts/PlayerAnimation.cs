using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animation animationComponent;
    [SerializeField] private AnimationType _currentAnimationType;
    public AnimationType CurrentAnimationType
    {
        get => _currentAnimationType;
        set
        {
            StartCoroutine(ChangeAnimSmoothly(value));
            _currentAnimationType = value;
        }
    }

    public void PlayAnimation(AnimationType animType, out float duration)
    {
        duration = animationComponent[animType.ToString()].length - 0.2f;
        StartCoroutine(ChangeAnimSmoothly(animType));
    }

    private IEnumerator ChangeAnimSmoothly(AnimationType type, float fadeTime = 0.2f)
    {
        animationComponent.Play(type.ToString());
        while (animationComponent.isPlaying && animationComponent[type.ToString()].time < animationComponent[type.ToString()].length - fadeTime)
        {
            yield return null;
        }
        animationComponent.CrossFade(_currentAnimationType.ToString(), fadeTime);
    }
}
public enum AnimationType
{
    Swim,
    SwimFast,
    TurnHorizontal,
    TurnVertical,
    Bite,
    Wrestle,
    Death,
    Hit,
    Beached,
    BeachedDeath
}