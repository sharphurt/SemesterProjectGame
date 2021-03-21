using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class TargetSelectorController : MonoBehaviour
    {
        [SerializeField] private string targetTag;

        public GameObject FindClosestTarget()
        {
            var targets = GameObject.FindGameObjectsWithTag(targetTag);
            var d = targets    
                .OrderBy(go => Vector3.Distance(gameObject.transform.position, go.transform.position))
                .FirstOrDefault();
            return d;
        }
    }
}