using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOriginEffect : AreaEffect {
    public override void Cast(Spell spell, Vector3 target)
    {
        this.spell = spell;
        Vector2 direction = ((Vector2)target - (Vector2)transform.position).normalized;
        Vector3 extents = GetComponent<Collider2D>().bounds.extents;
        //Debug.Log(direction.magnitude);
        //Debug.Log(direction);
        transform.position += (Vector3) direction * (extents.y + 0.5f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
