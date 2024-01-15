using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircleDB : MonoBehaviour, IDistanceBasedCircle
{
    public StatType Type { get; set; }
    public float Amount { get; set; }
    public Collider Collider { get; set; }

    public float GetCalculatedAmountByPos(Vector3 receiverPos)
    {
        float circleExtents = Collider.bounds.extents.x;
        
        float distance = Vector3.Distance(Collider.gameObject.transform.position, receiverPos);
        //Debug.Log("Distance is :" +distance+ " & Extent X is :" +circleExtents);
        
        var lerpMod = Mathf.InverseLerp(circleExtents, 0f, distance);
       // Debug.Log(Mathf.Lerp(1f, Amount, lerpMod));
        return Mathf.Lerp(1f, Amount, lerpMod) * -1;
    }
    private void Start()
    {
        Amount = 20;
        Type = StatType.Health;
        Collider = GetComponent<Collider>();
    }

}
