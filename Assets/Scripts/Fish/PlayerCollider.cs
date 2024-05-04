using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private Animation animationComponent;
    private void OnTriggerEnter(Collider other)
    {
        PlayAnimation(AnimationType.Bite);
        other.gameObject.SetActive(false);
    }

    private IEnumerator ChangeAnimSmoothly(string animName, float fadeTime)
    {
        animationComponent.Play(animName);
        while (animationComponent.isPlaying && animationComponent[animName].time < animationComponent[animName].length - fadeTime)
        {
            yield return null;
        }
        animationComponent.CrossFade("swim", fadeTime);
    }
    private void PlayAnimation(AnimationType animType)
    {
        string animName = GetAnimationName(animType);
        StartCoroutine(ChangeAnimSmoothly(animName, 0.3f));
    }
    private string GetAnimationName(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Swim:
                return "swim";
            case AnimationType.SwimFast:
                return "swim fast";
            case AnimationType.TurnHorizontal:
                return "turn horizontal";
            case AnimationType.TurnVertical:
                return "turn vertical";
            case AnimationType.Bite:
                return "bite";
            case AnimationType.Wrestle:
                return "wrestle";
            case AnimationType.Death:
                return "death";
            case AnimationType.Hit:
                return "hit";
            case AnimationType.Beached:
                return "beached";
            case AnimationType.BeachedDeath:
                return "beached death";
            default:
                return "";
        }
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
