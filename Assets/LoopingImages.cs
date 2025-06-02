using UnityEngine;

public class LoopingImages : MonoBehaviour
{
    public float speed = 5f; // Adjust the movement speed
    public RectTransform image1; // Reference to the first image
    public RectTransform image2; // Reference to the second image
    public RectTransform main; // Reference to the main
    public bool moving;
    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width;
        // Set the initial position of the second image (offscreen to the left)
        image1.anchoredPosition = new Vector2(-main.rect.width, image2.anchoredPosition.y);
    }

    void Update()
    {
        // Move the second image to the right
        image2.anchoredPosition += Vector2.right * speed * Time.deltaTime;
        // Move the first image to the right side
        image1.anchoredPosition += Vector2.right * speed * Time.deltaTime;     
        // Check if the right edge of the second image is offscreen
        if (image2.anchoredPosition.x > screenWidth)
        {
            image2.anchoredPosition = new Vector2(-main.rect.width, image2.anchoredPosition.y);
        }
        if (image1.anchoredPosition.x > screenWidth)
        {
            image1.anchoredPosition = new Vector2(-main.rect.width, image2.anchoredPosition.y);
        }
        //b
    }
}
