using UnityEngine;

namespace Components.ActionComponents
{
    public class EngineComponent : ActionComponent
    {
        public float Value => IsBroken ? debuffValue : 1;
        
        private bool isBroken;

        public bool IsBroken
        {
            get => isBroken;
            private set
            {
                isBroken = value;
                image.sprite = isBroken ? brokenIcon : normalIcon;
                OnPropertyChanged(nameof(IsBroken));
            }
        }
        public override void SetComponentBrokenness(float playerHealthPercent) => IsBroken = playerHealthPercent < 0.45;
    }
}