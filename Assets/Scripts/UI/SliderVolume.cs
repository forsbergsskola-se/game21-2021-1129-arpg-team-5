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
    
        void Update()
        {
            // times by 100 to get percentage number as normally between 0-1
            sliderValue = this.gameObject.GetComponent<Slider>().value * 100;
        
            // round to int when printing to get nice full number
            volume.SetText(Mathf.RoundToInt(sliderValue) + "");
        }
    }
}
