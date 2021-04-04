using UnityEngine;
using Utils;

namespace Entities
{
    public class Player : Entity
    {
        public Joystick joystick;

        public float acceleration;
        private Rigidbody2D rb;
        private Collider2D playerCollider;

        public delegate void PlayerDeathHandler(string killer);

        public event PlayerDeathHandler OnPlayerDeath;

        public override void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<Collider2D>();
            base.Start();
        }

        void FixedUpdate()
        {
            var screenEdges = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            var boundsHalfSize = playerCollider.bounds.size / 2f;
            rb.position = new Vector2(
                Mathf.Clamp(rb.position.x, -screenEdges.x + boundsHalfSize.x, screenEdges.x - boundsHalfSize.x),
                Mathf.Clamp(rb.position.y, -screenEdges.y + boundsHalfSize.y, screenEdges.y - boundsHalfSize.y));

            if (joystick.enabled)
            {
                rb.velocity = new Vector2(
                    joystick.Horizontal * acceleration + Input.GetAxis("Horizontal") * acceleration,
                    joystick.Vertical * acceleration + Input.GetAxis("Vertical") * acceleration);

                rb.rotation = Mathf.Asin(joystick.Horizontal) * -Mathf.PI * acceleration * 2;
            }
        }

        public override void Die()
        {
            base.Die();
            OnPlayerDeath?.Invoke("");
        }

        public void DisableJoystickControl()
        {
            joystick.gameObject.SetActive(false);
            rb.velocity = Vector2.zero;
            rb.rotation = 0;
        }
    }
}