using System.Linq;
using Entities;
using UnityEngine;

namespace Controllers
{
    public class TargetSelectorController : MonoBehaviour
    {
        [SerializeField] private string targetTag;

        public GameObject FindClosestTarget()
        {
            var targets = GameObject.FindGameObjectsWithTag(targetTag)
                .Where(o =>
                {
                    var enemy = o.GetComponent<Enemy>();
                    return enemy == null || enemy.isTarget;
                })
                .ToArray();

            var d = targets    
                .OrderBy(go => Vector3.Distance(gameObject.transform.position, go.transform.position))
                .FirstOrDefault();
            return d;
        }
    }
}