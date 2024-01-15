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
                ICircle circle = other.gameObject.GetComponent<ICircle>();
                //Debug.Log(other.gameObject.GetComponent<ICircle>().Amount);
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
