using System;
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

        private bool isCountingDown;
        private float startCountingDownTime;

        public Modifier(float duration, float value, string fieldName, SpriteRenderer spriteRenderer)
        {
            Duration = duration;
            Value = value;
            FieldName = fieldName;
            Icon = spriteRenderer.sprite;
        }

        public void StartTimer()
        {
            isCountingDown = true;
            startCountingDownTime = Time.time;
            Remained = Duration;
        }

        public bool IsOver => Remained <= 0;

        public void Update()
        {
            if (!isCountingDown)
                return;
            Remained = Duration - (Time.time - startCountingDownTime);
            if (IsOver)
            {
                isCountingDown = false;
                OnExpiration?.Invoke();
            }
        }
    }
}