using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour 
{
    float dayTime;
    int dayCount;
    public Transform sun;
    public Transform moon;
    public Animator clockAnimator;

    public SpriteRenderer dayRenderer;
    public SpriteRenderer nightRenderer;
    public float transitionDuration = 2.0f;
    private bool isDaytime;

    void Start()
    {
        dayCount = 0;
        dayTime = 0;
        UpdateBackground(true); // Initialize with day background if daytime
    }

    void Update()
    {
        dayTime += Time.deltaTime;
        if(dayTime >= 18)
        {   
            dayCount++; 
            dayTime = 0;
        }

        UpdateBackground(); // Smoothly transition the background if needed

        // Synchronize clock rotation with 24-hour cycle
        transform.localEulerAngles = Vector3.forward * ((360 * dayTime) / 18);

        dayRenderer.transform.rotation = Quaternion.identity;
        nightRenderer.transform.rotation = Quaternion.identity;
    }

    void UpdateBackground(bool instantTransition = false)
    {
        bool shouldBeDaytime = dayTime >= 8 && dayTime < 18;

        if (shouldBeDaytime != isDaytime) // If there is a change from day to night or vice versa
        {
            isDaytime = shouldBeDaytime;
            if (instantTransition)
            {
                dayRenderer.color = new Color(1, 1, 1, isDaytime ? 1 : 0);
                nightRenderer.color = new Color(1, 1, 1, isDaytime ? 0 : 1);
            }
            else
            {
                StartCoroutine(FadeBackgrounds(isDaytime));
            }
        }
    }

    IEnumerator FadeBackgrounds(bool toDaytime)
    {
        float elapsedTime = 0;

        float startDayAlpha = toDaytime ? 0 : 1;
        float startNightAlpha = toDaytime ? 1 : 0;

        float endDayAlpha = toDaytime ? 1 : 0;
        float endNightAlpha = toDaytime ? 0 : 1;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            dayRenderer.color = new Color(1, 1, 1, Mathf.Lerp(startDayAlpha, endDayAlpha, t));
            nightRenderer.color = new Color(1, 1, 1, Mathf.Lerp(startNightAlpha, endNightAlpha, t));

            yield return null;
        }

        dayRenderer.color = new Color(1, 1, 1, endDayAlpha);
        nightRenderer.color = new Color(1, 1, 1, endNightAlpha);
    }
}