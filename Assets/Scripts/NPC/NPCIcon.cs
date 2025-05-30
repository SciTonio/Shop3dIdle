using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class NPCIcon : MonoBehaviour
{
    public Image image;
    public Image completion;
    private float duration;
    private float elapsed;
    private Action callback;

    private void Start()
    {
        completion.transform.parent.gameObject.SetActive(false);
        Hide();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        if (duration > 0f)
        {
            elapsed += Time.deltaTime;
            completion.fillAmount = elapsed / duration;
            if (elapsed > duration)
            {
                duration = 0f;
                completion.transform.parent.gameObject.SetActive(false);
                callback?.Invoke();
            }
        }
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void SetImage(Sprite s) => image.sprite = s;

    public void StartCompletion(float d, Action cb = null)
    {
        elapsed = 0f;
        duration = d;
        callback = cb;
        completion.fillAmount = 0f;
        completion.transform.parent.gameObject.SetActive(true);
    }
}
