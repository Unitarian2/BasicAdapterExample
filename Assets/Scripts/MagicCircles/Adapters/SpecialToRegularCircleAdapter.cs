using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialToRegularCircleAdapter : ICircle
{
    Vector3 receiverPos;
    IDistanceBasedCircle distanceBasedCircle;
    public SpecialToRegularCircleAdapter(Vector3 receiverPos, StatType statType, float Amount, IDistanceBasedCircle distanceBasedCircle)
    {
        this.receiverPos = receiverPos;
        this.Type = statType;
        this.Amount = Amount;
        this.distanceBasedCircle = distanceBasedCircle;

    }
    public StatType Type { get; set; }
    public float Amount { get; set; }



    public float GetCalculatedAmount()
    {
        return distanceBasedCircle.GetCalculatedAmountByPos(receiverPos);
    }
}
