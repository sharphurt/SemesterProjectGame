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

        public void StartCoinMove(Vector3 initial, float delay)
        {
            var targetPos =
                Camera.main.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y));

            StartCoroutine(MoveCoin(initial, targetPos, delay));
        }

        private IEnumerator MoveCoin(Vector2 start, Vector2 end, float delay)
        {

            float time = 0;
            yield return new WaitForSeconds(delay);
            var coin = Instantiate(coinPrefab, start, Quaternion.identity);

            while (time < 1)
            {
                time += speed * Time.deltaTime;
                coin.transform.position = Vector2.Lerp(start, end, time);
                yield return new WaitForEndOfFrame();
            }

            gameManager.Score++;
            Destroy(coin.gameObject);
        }
    }
}