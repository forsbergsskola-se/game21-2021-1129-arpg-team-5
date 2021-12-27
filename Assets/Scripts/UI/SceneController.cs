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

        // load scene via name
        
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"{sceneName} loaded");
        }
        
        // quit game (works in build)
    
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit Button: Pressed");
        }
        
        // pause game & deactivate mouse controller

        public void PauseGame()
        {
            pauseMenu = GameObject.FindWithTag("Pause Menu");
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseController>().enabled = false;;
        }
        
        // resume game & reenable mouse controller

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
        
        // possible way to mute D-MOD audio later
        
        public void MuteUnmuteBus(bool setMuted){

            //masterBus.setMute(setMuted);
        }
    }   
}