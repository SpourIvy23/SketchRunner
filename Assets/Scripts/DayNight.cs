using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance { get; private set; }  // Singleton instance

    float dayTime;
    int dayCount;
    public Transform sun;
    public Transform moon;
    public Animator clockAnimator;

    public SpriteRenderer dayRenderer;
    public SpriteRenderer nightRenderer;
    public float transitionDuration = 2.0f;
    private bool isDaytime;

    public float timeScale = 0.5f; // Set to 0.5 to double the duration of day and night

    void Awake()
    {
        // Ensure there is only one instance of Clock
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dayCount = 0;
        dayTime = 12; // Start at 6 AM to begin during daytime
        UpdateBackground(true); // Initialize the background immediately to match daytime
    }

    void Update()
    {
        dayTime += Time.deltaTime * timeScale;
        if(dayTime >= 24)
        {   
            dayCount++; 
            dayTime = 0;
        }

        UpdateBackground(); 

        transform.localEulerAngles = Vector3.forward * ((360 * dayTime) / 24);

        dayRenderer.transform.rotation = Quaternion.identity;
        nightRenderer.transform.rotation = Quaternion.identity;
    }

    public bool IsDaytime()
    {
        return isDaytime;
    }

    void UpdateBackground(bool instantTransition = false)
    {
        bool shouldBeDaytime = dayTime >= 6 && dayTime < 18;

        if (shouldBeDaytime != isDaytime)
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
