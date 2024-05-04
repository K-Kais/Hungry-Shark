using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animation animationComponent;
    public AnimationType CurrentAnimationType = AnimationType.Swim;

    public void PlayAnimation(AnimationType animType) => StartCoroutine(ChangeAnimSmoothly(animType, 0.3f));
    private IEnumerator ChangeAnimSmoothly(AnimationType type, float fadeTime)
    {
        animationComponent.Play(type.ToString());
        while (animationComponent.isPlaying && animationComponent[type.ToString()].time < animationComponent[type.ToString()].length - fadeTime)
        {
            yield return null;
        }
        animationComponent.CrossFade(CurrentAnimationType.ToString(), fadeTime);
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