using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//attention a la méthode StopAllCoroutine qui intéromp l'execution de ces coroutines si appellé sur le même monobehavior
public static class DelayInstructionsExtensions
{
    public static Coroutine DelayExecution(this MonoBehaviour mb, int frameNumber, List<Action> actions)
    {
        IEnumerator coroutine()
        {
            foreach (var action in actions)
            {
                action.Invoke();
                for (int i = 0; i < frameNumber; i++) yield return null;
            }
        }

        return mb.StartCoroutine(coroutine());
    }

    public static Coroutine InvokeActionWithDelay(this MonoBehaviour mb, UnityAction action, float seconds)
    {
        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSecondsRealtime(seconds);
            action?.Invoke();
        }

        return mb.StartCoroutine(WaitCoroutine());
    }

    public static Coroutine SetTimetOut(this MonoBehaviour mb, int numberFrame, UnityAction lambda)
    {
        IEnumerator coroutine(int frameNumber)
        {
            for (int i = 0; i < frameNumber; i++) yield return null;
            lambda.Invoke();
        }

        return mb.StartCoroutine(coroutine(numberFrame));
    }
    public static Coroutine SetTimetOut(this MonoBehaviour mb, float seconds, UnityAction lambda, bool timeDependant = false)
    {
        IEnumerator coroutine(float seconds)
        {
            yield return timeDependant ? new WaitForSeconds(seconds) : new WaitForSecondsRealtime(seconds);
            lambda.Invoke();
        }

        return mb.StartCoroutine(coroutine(seconds));
    }

    public static Coroutine InstructionWithDelayXtimes(this MonoBehaviour mb, float seconds, int xtimes, UnityAction lambda)
    {
        int nb = 0;
        IEnumerator coroutine(float seconds)
        {
            while (nb < xtimes)
            {
                lambda.Invoke();
                nb++;
                yield return new WaitForSecondsRealtime(seconds);
            }
        }

        return mb.StartCoroutine(coroutine(seconds));
    }

    public static Coroutine InstructionEachFrame(this MonoBehaviour mb, float length, UnityAction lambda)
    {
        IEnumerator coroutine(float duration)
        {
            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                lambda.Invoke();
                yield return null; // Wait until the next frame
            }
        }

        return mb.StartCoroutine(coroutine(length));
    }

    public static Coroutine InstructionWithDelayXtimes(this MonoBehaviour mb, int frames, int xtimes, UnityAction lambda)
    {
        int nb = 0;
        IEnumerator coroutine()
        {
            while (nb < xtimes)
            {
                lambda.Invoke();
                nb++;
                for (int i = 0; i < frames; i++) yield return null;
            }
        }

        return mb.StartCoroutine(coroutine());
    }

    public static Coroutine InstructionRepeatingWithLength(this MonoBehaviour mb, int frames, float nbOccurences, UnityAction lambda)
    {
        float nb = 0;
        IEnumerator coroutine()
        {
            while (nb < nbOccurences)
            {
                lambda.Invoke();
                nb += Time.deltaTime;
                for (int i = 0; i < frames; i++) yield return null;
            }
        }

        return mb.StartCoroutine(coroutine());
    }

    public static Coroutine SetInterval(this MonoBehaviour mb, float seconds, UnityAction lambda, bool timeDependant = false)
    {
        IEnumerator coroutine(float seconds)
        {
            while (true)
            {
                yield return timeDependant ? new WaitForSeconds(seconds) : new WaitForSecondsRealtime(seconds);
                lambda.Invoke();
            }
        }

        return mb.StartCoroutine(coroutine(seconds));
    }

    public static Coroutine SetInterval(this MonoBehaviour mb, int frame, UnityAction lambda, bool timeDependant = false)
    {
        IEnumerator coroutine(int seconds)
        {
            while (true)
            {
                for (int i = 0; i < frame; i++)
                    yield return null;
                lambda.Invoke();
            }
        }

        return mb.StartCoroutine(coroutine(frame));
    }

    public static Coroutine SetInterval(this MonoBehaviour mb, Func<float> seconds, UnityAction lambda)
    {
        IEnumerator coroutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(seconds.Invoke());
                lambda.Invoke();
            }
        }

        return mb.StartCoroutine(coroutine());
    }
}