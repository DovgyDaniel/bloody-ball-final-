using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class паузаменю : MonoBehaviour
{

    public bool PauseGame;
    public GameObject pauseGameMenu;

    void Update()     
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame =false;
    }
    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame =true;
    }
    public void LostMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        var musicManager = FungusManager.Instance.MusicManager;

        musicManager.StopMusic();

        

    }   
}
