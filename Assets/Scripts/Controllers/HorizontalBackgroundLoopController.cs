using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBackgroundLoop : MonoBehaviour
{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(
            Screen.width, Screen.height, mainCamera.transform.position.z));
        
        foreach (var obj in levels)
            LoadChildObjects(obj);
    }

    private void LoadChildObjects(GameObject obj)
    {
        var objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        var childNeeded = (int) Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        var clone = Instantiate(obj);
        
        for (var i = 0; i <= childNeeded; i++)
        {
            var c = Instantiate(clone, obj.transform, true);
            var position = obj.transform.position;
            c.transform.position = new Vector3(objectWidth * i, position.y, position.z);
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
            var halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                var position = lastChild.transform.position;
                firstChild.transform.position = new Vector3(position.x + halfObjectWidth * 2, position.y, position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                var position = firstChild.transform.position;
                lastChild.transform.position = new Vector3(position.x - halfObjectWidth * 2, position.y, position.z);
            }
        }
    }

    void Update()
    {
        var velocity = Vector3.zero;
        var desiredPosition = transform.position + new Vector3(scrollSpeed, 0, 0);
        var smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;
    }

    private void LateUpdate()
    {
        foreach (GameObject obj in levels)
            RepositionChildObjects(obj);
    }
}