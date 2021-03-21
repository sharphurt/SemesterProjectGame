using UnityEngine;

namespace Controllers
{
    public class VerticalBackgroundLoopController : MonoBehaviour
    {
        public GameObject[] levels;
        public Camera mainCamera;
        private Vector2 screenBounds;
        public float choke;
        public float scrollSpeed;

        private void Start()
        {
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(
                Screen.width, Screen.height, mainCamera.transform.position.z));

            foreach (var obj in levels)
                LoadChildObjects(obj);
        }

        private void LoadChildObjects(GameObject obj)
        {
            var objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
            var childNeeded = (int) Mathf.Ceil(screenBounds.y * 2 / objectHeight);
            var clone = Instantiate(obj);

            for (var i = 0; i <= childNeeded; i++)
            {
                var c = Instantiate(clone, obj.transform, true);
                var position = obj.transform.position;
                c.transform.position = new Vector3(position.x, objectHeight, position.z);
                c.name = obj.name + i;
            }

            Destroy(clone);
            Destroy(obj.GetComponent<SpriteRenderer>());
        }

        private void RepositionChildObjects(GameObject obj)
        {
            var children = obj.GetComponentsInChildren<Transform>();
            var firstChild = children[1].gameObject;
            var lastChild = children[children.Length - 1].gameObject;
            if (children.Length > 1)
            {
                var halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;

                if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjectHeight)
                {
                    firstChild.transform.SetAsLastSibling();
                    var position = lastChild.transform.position;
                    firstChild.transform.position = new Vector3(position.x, position.y  + halfObjectHeight * 2, position.z);
                }
                else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjectHeight)
                {
                    lastChild.transform.SetAsFirstSibling();
                    var position = firstChild.transform.position;
                    lastChild.transform.position = new Vector3(position.x, position.y - halfObjectHeight * 2, position.z);
                }
            }
        }

        private void Update()
        {
            foreach (var obj in levels)
                obj.transform.position += new Vector3(0, scrollSpeed, 0) * Time.deltaTime;
        
        }

        private void LateUpdate()
        {
            foreach (var obj in levels)
                RepositionChildObjects(obj);
        }
    }
}