using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ProgressBarWithIconController : ProgressBarController
    {
        public Image icon;
        
        public void SetIcon(Sprite image) => icon.sprite = image;
    }
}