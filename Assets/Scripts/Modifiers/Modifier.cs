using System;
using UnityEngine;

namespace Modifiers
{
    public class Modifier
    {
        public float duration;
        public float remained;
        public float value;
        public string fieldName;
        
        public bool IsOver => Math.Abs(remained) < 0.01;

        public delegate void ModifierExpirationHandler();

        public event ModifierExpirationHandler OnExpiration;
        
        private bool isCountingDown;
        private float startCountingDownTime;
        
        public Modifier(float duration, float value, string fieldName)
        {
            this.duration = duration;
            this.value = value;
            this.fieldName = fieldName;
        }

        public void StartTimer()
        {
            isCountingDown = true;
            startCountingDownTime = Time.time;
            remained = duration;
        }

        public void Update()
        {
            if (!isCountingDown)
                return;
            remained = duration - (Time.time - startCountingDownTime);
            if (Math.Abs(remained) < 0.01)
            {
                isCountingDown = false;
                OnExpiration?.Invoke();
            }
        }
    }
}