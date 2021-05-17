using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CircularProgressBarWithIcon : CircularProgressBarController
    {
        public Image icon;
        
        public void SetIcon(Sprite image) => icon.sprite = image;
    }
}