using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Team5.Ui
{
    public class SceneController : MonoBehaviour
    {
        private GameObject pauseMenu;

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"{sceneName} loaded");
        }
    
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit Button: Pressed");
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            Debug.Log("Game Paused");
        }

        public void ResumeGame()
        {
            pauseMenu = GameObject.FindWithTag("Pause Menu");
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
            }
            Time.timeScale = 1;
            Debug.Log("Game Resumed");
        }
        
        public void MuteUnmuteBus(bool setMuted){

            //masterBus.setMute(setMuted);
        }
    }   
}