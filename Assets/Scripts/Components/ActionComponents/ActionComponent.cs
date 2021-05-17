using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Components.ActionComponents
{
    public abstract class ActionComponent: MonoBehaviour, INotifyPropertyChanged
    {
        public float debuffValue;
        
        public Sprite normalIcon;
        public Sprite brokenIcon;

        protected Image image;
        
        private void Start()
        {
            image = GetComponent<Image>();
        }

        public abstract void SetComponentBrokenness(float playerHealthPercent);
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}