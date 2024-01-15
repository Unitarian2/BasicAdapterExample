using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveInteraction : Subject
{
    bool isInteractable;

    private void Start()
    {
        isInteractable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isInteractable)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("ReceiveInteractable"))
            {
                ICircle circle = null;
                if (TryGetComponent<ICircle>(out ICircle getCircle))
                {
                    circle = getCircle;    
                }

                if (TryGetComponent<IDistanceBasedCircle>(out IDistanceBasedCircle getSpecialCircle))
                {
                    ICircle adapter = new SpecialToRegularCircleAdapter(this.gameObject.transform.position, getCircle.Type,getCircle.Amount, getSpecialCircle);
                    circle = adapter;             
                }

                NotifyObservers(circle.Type, circle.GetCalculatedAmount());
                isInteractable = false;
                StartCoroutine(ReceiveInteractCooldown());

            }
        }
    }

    IEnumerator ReceiveInteractCooldown()
    {
        yield return new WaitForSeconds(1);
        isInteractable = true;
    }
}
