using UnityEngine;

public class Gun: MonoBehaviour, IShootable
{
    public Bullet bullet;
    public float power;

    public Collider2D parentCollider;

    [HideInInspector]
    public Transform firePoint;
    
    public void Shoot(Transform tg)
    {
        var position = firePoint.position;
        var dir = (tg.position - position).normalized;
        var facing = Quaternion.LookRotation(dir);
        facing.x = facing.y = 0;
        var instantiated = Instantiate(bullet, position, facing);
        var rb = instantiated.GetComponent<Rigidbody2D>();    
        rb.velocity = dir * power;
        instantiated.parentCollider = parentCollider;
    }
}