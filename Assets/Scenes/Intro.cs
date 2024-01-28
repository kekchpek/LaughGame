using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public List<Image> introImages;
    public Image frame;
    public float fadeDuration = 1.0f;

    private int currentIndex = 0;
    private bool skipImage = false;
    private float imageDisplayTime = 3.0f;

    void Start()
    {
        StartCoroutine(ShowIntro());
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            skipImage = true;
    }

    private IEnumerator ShowIntro()
    {
        foreach (Image image in introImages)
        {
            if (currentIndex == 5) frame.gameObject.SetActive(false);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            image.gameObject.SetActive(true);
            float timer = 0.0f;

            while (timer < fadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

                timer += Time.deltaTime;

                if (skipImage)
                {
                    break;
                }

                yield return null;
            }

            if (!skipImage)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                yield return new WaitForSeconds(imageDisplayTime);
            }

            image.gameObject.SetActive(false);

            if(currentIndex == 0) frame.gameObject.SetActive(true); 
            

            currentIndex++;
            skipImage = false;
        }

        SceneManager.LoadScene("MainMenu");

    }
}
