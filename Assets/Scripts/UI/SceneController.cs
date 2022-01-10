using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Control;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.WebCam;

namespace Team5.Ui
{
    public class SceneController : MonoBehaviour
    {
        private string Scene;
        private GameObject pauseMenu;
        private GameObject optionsMenu;
        private string sceneName;

        private void OnGUI()
        {
            Scene scene = SceneManager.GetActiveScene();

            
            if (scene.name == "Build_Test")
            {
                pauseMenu = FindObjectOfType<HUD>().PauseMenu;
                optionsMenu = FindObjectOfType<HUD>().OptionsMenu;
            }
        }

        private void Update()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "Video_Intro")
            {
                if (Input.GetKeyDown("space"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Debug.Log("next scene loaded");                }
            }
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
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseController>().enabled = false;;
        }
        
        // resume game & reenable mouse controller

        public void ResumeGame()
        {
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
        
        // opens and closes options menu

        public void OptionsOpen()
        {
            var OptionsMenu = GameObject.FindGameObjectWithTag("Pause Button").transform.Find("Options Menu").gameObject;
            OptionsMenu.SetActive(true);
            MainMenuClick();
        }

        public void OptionsClose()
        {
            var OptionsMenu = GameObject.FindGameObjectWithTag("Pause Button").transform.Find("Options Menu").gameObject;
            OptionsMenu.SetActive(false);
            MainMenuClick();
        }

        public void CreditsOpen()
        {
            var Credits = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Credits Menu").gameObject;
            Credits.SetActive(true);
        }

        public void CreditsClose()
        {
            var Credits = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Credits Menu").gameObject;
            Credits.SetActive(false);
        }


        // special button for Main Menu && also works for toggling Pause Menu while in Options Menu

        public void MainMenuClick()
        {
            var PauseMenu = GameObject.FindGameObjectWithTag("Pause Button").transform.Find("Pause Menu").gameObject;
            if (PauseMenu.activeInHierarchy)
            {
                PauseMenu.SetActive(false);
            }
            else
            {
                PauseMenu.SetActive(true);
            }
            PauseGame();
        }
        
        // possible way to mute D-MOD audio later
        
        public void MuteUnmuteBus(bool setMuted){

            //masterBus.setMute(setMuted);
        }
    }   
}