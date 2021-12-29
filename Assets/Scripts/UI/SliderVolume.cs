using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Team5.Ui
{
    public class SliderVolume : MonoBehaviour
    {
        public TMP_Text volume;
        private float sliderValue;
        private float sliderVolume;
        private float mute = 0;

        string masterBusString = "Bus:/";
        FMOD.Studio.Bus masterBus;

        private void Awake()
        {
            masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        }
        
        void Update()
        {
            // access Slider component's value
            sliderValue = this.gameObject.GetComponent<Slider>().value;
            
            // times by 100 to get percentage number as normally between 0-1
            sliderVolume = sliderValue * 100;
        
            // round to int when printing to get nice full number
            volume.SetText(Mathf.RoundToInt(sliderVolume) + "");
            
            // apply to master volume
            masterBus.setVolume(sliderValue);
            masterBus.getVolume(out sliderValue);
        }
    }
}
