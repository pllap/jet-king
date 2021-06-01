using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CshItemCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            CshGameManager.Instance.fuel = CshGameManager.Instance.fuelCapacity;
            CshGameManager.Instance.fuelImage.fillAmount = CshGameManager.Instance.fuel / CshGameManager.Instance.fuelCapacity;
            Destroy(this.gameObject);
        }
    }
}
