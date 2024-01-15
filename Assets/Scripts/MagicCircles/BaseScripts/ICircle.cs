using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICircle
{
    public StatType Type { get; set; }
    public float Amount {get; set;}
    public float GetCalculatedAmount();
}
