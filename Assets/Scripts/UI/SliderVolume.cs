using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Team5.Ui
{
    public class SliderVolume : MonoBehaviour
    {
        private TMP_Text volumeText;
        public GameObject slider;
        
        private float sliderValue;
        private float sliderVolume;
        private float mute = 0;
        private bool muteToggle = false;
        private float oldSliderValue;
        
        string masterBusString = "Bus:/";
        FMOD.Studio.Bus masterBus;

        private void Awake()
        {
            masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
            volumeText = slider.GetComponentInChildren<TMP_Text>();
        }
        
        void Update()
        {

            // access Slider component's value
            sliderValue = slider.GetComponent<Slider>().value;
            
            // times by 100 to get percentage number as normally between 0-1
            sliderVolume = sliderValue * 100;
        
            // round to int when printing to get nice full number
            volumeText.SetText(Mathf.RoundToInt(sliderVolume) + "");
            
            // apply to master volume
            masterBus.setVolume(sliderValue);
            masterBus.getVolume(out sliderValue);
        }

        public void MuteEverythingToggle()
        {
            if (muteToggle == false)
            {
                MuteEverything();
            }

            else
            {
                UnMute();
            }
        }
        
        public void MuteEverything()
        {
            oldSliderValue = sliderValue;
            sliderValue = mute;
            muteToggle = true;

        }

        public void UnMute()
        {
            sliderValue = oldSliderValue;
            muteToggle = false;
        }
    }
}
