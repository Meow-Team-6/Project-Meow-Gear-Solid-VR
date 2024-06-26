using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject controlsMenu2;
    public GameObject startingMenu;
    float previousTimeScale = 1;
    public static bool isPaused;
    public AudioSource source;
    public AudioClip pauseSound;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButton("Pause"))
        {
            Debug.Log("Pause");
            TogglePause();
        }*/
    }

public void TPause(InputAction.CallbackContext Context)
{
   TogglePause();
}

    public void TogglePause()
    {
        if(Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            source.PlayOneShot(pauseSound, .75f);
            isPaused = true;
            pauseMenu.SetActive(true);
            startingMenu.SetActive(true);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            controlsMenu2.SetActive(false);
            EventBus.Instance.OpenInventory();
        }
        else if (Time.timeScale == 0)
        {
            pauseMenu.SetActive(false);
            startingMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            controlsMenu2.SetActive(false);
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
            EventBus.Instance.CloseInventory();
        }
    }

    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
}
