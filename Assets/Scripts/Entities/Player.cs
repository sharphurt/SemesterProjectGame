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

        private float count;
        private Vector2 middlePoint;
        private Vector2 startPosition;

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

                rb.rotation = Mathf.Asin(joystick.Horizontal /** joystick.Vertical*/) * -Mathf.PI * acceleration * 2;
            }
        }

        protected override void UpdatePosition()
        {
            if (count < 1.0f && MovingToPoint)
            {
                count += .3f * Time.deltaTime;

                Vector3 m1 = Vector2.Lerp(startPosition, middlePoint, count);
                Vector3 m2 = Vector2.Lerp(middlePoint, TargetPosition, count);
                transform.position = Vector2.Lerp(m1, m2, count);
                var rotation = Vector2Utils.CalculateFacingToTarget(m1, m2).angle;
                transform.rotation = rotation;
            }
        }

        public override void MoveTo(Vector2 targetPos, float speed)
        {
            MovingToPoint = true;
            startPosition = transform.position;
            middlePoint = startPosition + (targetPos - startPosition) / 2 +
                          Vector2.left * ((startPosition.x - targetPos.x) * 0.6f);
            TargetPosition = targetPos;
            MoveSpeed = speed;
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