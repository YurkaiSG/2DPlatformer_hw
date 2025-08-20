using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected float ChangeSpeed = 80f;
    protected Slider Slider;
    protected Coroutine SliderChangeRoutine;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected virtual void Init(float minSliderValue = 0, float maxSliderValue = 1, float startSliderValue = 1)
    {
        Slider.minValue = minSliderValue;
        Slider.maxValue = maxSliderValue;
        Change(startSliderValue);
    }

    protected virtual void ChangeView(float targetValue)
    {
        if (SliderChangeRoutine != null)
            StopCoroutine(SliderChangeRoutine);
        
        SliderChangeRoutine = StartCoroutine(ChangeSmoothly(targetValue));
    }

    protected void Change(float targetValue)
    {
        Slider.value = Mathf.Clamp(targetValue, Slider.minValue, Slider.maxValue);
    }

    protected IEnumerator ChangeSmoothly(float targetValue)
    {
        while (Slider.value != targetValue && enabled)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, targetValue, ChangeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
