using System.Collections;
using System.Collections.Generic;
using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class Intro : MonoBehaviour
{
    public List<Image> introImages;
    public float fadeDuration = 1.5f;
    public List<float> _displayTimes = new ();

    private int currentIndex = 0;
    private bool skipImage = false;

    private IAudioManager _audioManager;
    
    [Inject]
    public void Construct(
        IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    
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
        bool isFirstOrSecondImage = false; 

        foreach (Image image in introImages)
        {
            if (currentIndex > 0)
            {
                _audioManager.Play(_audioManager.AudioConfig.Intro[currentIndex-1]);
            }

            image.gameObject.SetActive(true);

            if (currentIndex < 2)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
                float timer = 0.0f;

                while (timer < fadeDuration)
                {
                    float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

                    image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

                    timer += Time.deltaTime;

                    if (Input.anyKeyDown)
                    {
                        break; 
                    }

                    yield return null;
                }

                if (!Input.anyKeyDown)
                {
                    yield return new WaitForSeconds(_displayTimes[currentIndex]);
                }

                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }
            else
            {
               
                float timer = 0.0f;
                while (timer < _displayTimes[currentIndex])
                {
                    if (Input.anyKeyDown)
                    {
                        break; 
                    }
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            image.gameObject.SetActive(false);
            
            currentIndex++;
        }

        SceneManager.LoadScene("MainMenu");

    }
}
