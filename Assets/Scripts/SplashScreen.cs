using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace splashScreen
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private Image myImage;
        [Header("Cursor")]
        [SerializeField] private Texture2D cursorTexture;
        private Vector2 cursorHotSpot;

        void Awake()
        {
            cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);

            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0f);
            StartCoroutine(FadeEffect());

            #if UNITY_EDITOR
                        PlayerPrefs.DeleteAll();
            #endif

            if (PlayerPrefs.GetInt("FirstTimeOpenGame") == 0)
            {
                //*OPTIONS*//

                //Sound
                PlayerPrefs.SetFloat("OptionMasterVolume", 1);
                PlayerPrefs.SetFloat("OptionEffectsVolume", 1);
                PlayerPrefs.SetFloat("OptionMusicVolume", 1);

                //Video
                PlayerPrefs.SetInt("OptionFullScreen", 1);
                PlayerPrefs.SetInt("OptionVSync", 1);
                PlayerPrefs.SetInt("OptionResolution", 1);
                PlayerPrefs.SetInt("OptionLimitFPS", 0);

                //Control
                PlayerPrefs.SetFloat("mouseHorizontalX", 0.5f);
                PlayerPrefs.SetFloat("mouseVerticalY", 0.5f);

                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = 60;
                Screen.SetResolution(1920, 1080, true);

                //Scores
                PlayerPrefs.SetInt("BestScore", 0);

                PlayerPrefs.SetInt("FirstTimeOpenGame", 1);
            }

        }

        private IEnumerator FadeEffect()
        {
            float fadeCount = 0;

            while (fadeCount < 1.0f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, fadeCount);
            }

            while (fadeCount > 0.01f)
            {
                fadeCount -= 0.01f;
                yield return new WaitForSeconds(-0.01f);
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, fadeCount);
            }

            //Go to Next Scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
