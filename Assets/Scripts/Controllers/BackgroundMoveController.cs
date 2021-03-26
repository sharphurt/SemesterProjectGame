using System;
using UnityEngine;

namespace Controllers
{
    public class BackgroundMoveController : MonoBehaviour
    {
        private float scrollOffset;

        private void Start()
        {
            
            scrollOffset = GetComponent<SpriteRenderer>().bounds.size.y;
            Debug.Log(GetComponent<SpriteRenderer>().bounds.size.y);
        }


        private void Update() =>
            transform.position += new Vector3(0, -GameManager.MovementSpeed * Time.deltaTime, 0);

        private void LateUpdate()
        {
            var position = transform.position;
            if (Mathf.Abs(position.y) >= scrollOffset * transform.childCount)
                transform.SetPositionAndRotation(new Vector3(position.x, 0, position.z), transform.rotation);
        }
    }
}