using System.Collections;
using UnityEngine;
using DentedPixel;

public class ProgressionBar : MonoBehaviour
{
    public GameObject bar;
    public GameObject barContainer;

    // Start is called before the first frame update
    void Start()
    {
        // Initially, hide the progression bar.
        SetProgressBarVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the progression bar is currently animating.
        bool isAnimating = LeanTween.isTweening(bar);

        // Update the visibility of the progression bar.
        SetProgressBarVisibility(isAnimating);
    }

    public void ProgressionBarAnimation(float time)
    {
        LeanTween.scaleX(bar, 1f, time).setOnComplete(() =>
        {
            // Animation to 1 is complete; reset the progression bar to 0.
            SetProgressBarScale(0f);
        });
    }

    private void SetProgressBarVisibility(bool isVisible)
    {
        // Set the visibility of the bar GameObject.
        if (bar != null)
        {
            barContainer.SetActive(isVisible);
        }
    }

    private void SetProgressBarScale(float scale)
    {
        if (bar != null)
        {
            Vector3 newScale = bar.transform.localScale;
            newScale.x = scale;
            bar.transform.localScale = newScale;
        }
    }
}
