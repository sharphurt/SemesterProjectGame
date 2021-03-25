using UnityEngine;

namespace Utils
{
    public class UpdateCaller : MonoBehaviour
    {
        private static UpdateCaller _instance;
        public static System.Action OnUpdate;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else if (this != _instance)
                Destroy(this);
        }

        private void Update() => OnUpdate?.Invoke();
    }
}