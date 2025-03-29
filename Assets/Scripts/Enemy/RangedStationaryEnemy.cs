using UnityEngine;

public class RangedStationaryEnemy : BaseEnemy
{
    [SerializeField] float range;
    protected override void HandleCombat()
    {
        
    }

    protected override void HandleMovement()
    {
        Vector3 pos = target.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
    }
}
