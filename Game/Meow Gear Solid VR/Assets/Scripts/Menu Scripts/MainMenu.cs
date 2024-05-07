using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ScreenFader fader;
    public ScreenFader faderFrom;
    public GameObject startMenu;
    public GameObject controlsMenu;
    public GameObject controlsMenu2;
    public AudioSource source;
    public bool noStart;
    void Start()
    {
        controlsMenu.SetActive(false);
        controlsMenu2.SetActive(false);
        if(noStart == false)
        {
            Time.timeScale = 0;
            startMenu.SetActive(true);
            EventBus.Instance.LevelLoadStart();            
        }
        else
        {
            startMenu.SetActive(false);
            faderFrom.GetComponentInParent<GameObject>().SetActive(false);
        }

    }
    public void StartGame ()
    {
        Debug.Log("Starting Game");
        Time.timeScale = 1;
        EventBus.Instance.GameStart();
        StartCoroutine(FadeOut(source, 2f));
    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    IEnumerator FadeOut(AudioSource source, float duration) 
    {
        EventBus.Instance.LevelLoadEnd();
        float startVolume = source.volume;
        fader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(.25f);
        startMenu.SetActive(false);
        Debug.Log("Entering next level");
        faderFrom.FadeFromBlack(duration);
        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
 
            yield return null;
        }
 
        source.Stop();
        source.volume = startVolume;
    }
    
}
