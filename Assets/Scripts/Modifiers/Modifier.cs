using System;
using System.Timers;
using UnityEngine;

namespace Modifiers
{
    public class Modifier
    {
        public readonly float Duration;
        public float Remained;
        public readonly float Value;
        public readonly string FieldName;
        public readonly Sprite Icon;

        public delegate void ModifierExpirationHandler();

        public event ModifierExpirationHandler OnExpiration;

        private Timer timer = new Timer(1000);

        public Modifier(float duration, float value, string fieldName, SpriteRenderer spriteRenderer)
        {
            Duration = duration;
            Value = value;
            FieldName = fieldName;
            Icon = spriteRenderer.sprite;
            timer.Elapsed += (sender, args) => OnTimerTick();
        }

        public void StartTimer()
        {
            timer.Stop();
            Remained = Duration;
            timer.Start();
        }

        public bool IsOver => Remained <= 0;

        private void OnTimerTick()
        {
            Remained--;
            if (!IsOver) return;
            timer.Stop();
            OnExpiration?.Invoke();
        }

        public void Reset() => StartTimer();
    }
}