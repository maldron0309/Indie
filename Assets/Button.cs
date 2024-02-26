using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.1f;
    public float animationDuration = 0.5f;

    private Vector3 originalScale;
    private RectTransform rectTransform;
    private bool isAnimating = false;

    void Start()
    {
        originalScale = transform.localScale;
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isAnimating)
        {
            StartScaleAnimation(scaleFactor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isAnimating)
        {
            StartScaleAnimation(1.0f);
        }
    }

    void StartScaleAnimation(float targetScaleFactor)
    {
        StartCoroutine(ScaleOverTime(targetScaleFactor));
    }

    System.Collections.IEnumerator ScaleOverTime(float targetScaleFactor)
    {
        isAnimating = true;

        Vector3 targetScale = originalScale * targetScaleFactor;

        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float scaleX = Mathf.Lerp(rectTransform.localScale.x, targetScale.x, t);
            float scaleY = Mathf.Lerp(rectTransform.localScale.y, targetScale.y, t);
            rectTransform.localScale = new Vector3(scaleX, scaleY, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;
        isAnimating = false;
    }
}
