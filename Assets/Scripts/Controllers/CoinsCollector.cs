using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class CoinsCollector : MonoBehaviour
    {
        public float speed;
        public Transform target;
        public GameObject coinPrefab;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GetComponent<GameManager>();
        }

        public void StartCoinMove(Vector3 initial)
        {
            var targetPos =
                Camera.main.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y));

            var coin = Instantiate(coinPrefab, new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity);
            StartCoroutine(MoveCoin(coin.transform, initial, targetPos));
        }

        private IEnumerator MoveCoin(Transform obj, Vector2 start, Vector2 end)
        {
            float time = 0;

            while (time < 1)
            {
                time += speed * Time.deltaTime;
                obj.position = Vector2.Lerp(start, end, time);
                yield return new WaitForEndOfFrame();
            }

            gameManager.Score++;
            Destroy(obj.gameObject);
        }
    }
}