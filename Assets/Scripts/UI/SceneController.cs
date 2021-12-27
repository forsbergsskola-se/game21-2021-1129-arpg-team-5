using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Control;
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
            pauseMenu = GameObject.FindWithTag("Pause Menu");
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseController>().enabled = false;;
        }

        public void ResumeGame()
        {
            pauseMenu = GameObject.FindWithTag("Pause Menu");
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseController>().enabled = true;;
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                return;
            }
        }
        
        public void MuteUnmuteBus(bool setMuted){

            //masterBus.setMute(setMuted);
        }
    }   
}