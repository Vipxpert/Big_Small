using UnityEngine;

public class HeadIndicator : MonoBehaviour
{
    public GameObject indicatorObject;

    private void Start()
    {
        // Ensure the indicatorObject is initially inactive
        if (indicatorObject != null)
        {
            indicatorObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the trigger collider
            ShowIndicator();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the trigger collider
            HideIndicator();
        }
    }

    private void ShowIndicator()
    {
        // Check if the indicatorObject is assigned
        if (indicatorObject != null)
        {
            // Activate the indicatorObject
            indicatorObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("IndicatorObject not assigned in the HeadIndicator script!");
        }
    }

    private void HideIndicator()
    {
        // Check if the indicatorObject is assigned
        if (indicatorObject != null)
        {
            // Deactivate the indicatorObject
            indicatorObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("IndicatorObject not assigned in the HeadIndicator script!");
        }
    }
}
