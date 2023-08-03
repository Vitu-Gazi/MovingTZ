using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GettingZone : MonoBehaviour
{
    [SerializeField] private GameObject collectObject;
    [SerializeField] private float gettingTime = 1;

    private float currentTime;

    private IDisposable gettingTimer;
    private ICollector currentCollector;

    private void Start()
    {
        currentTime = gettingTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out currentCollector))
        {
            GameObject obj = Instantiate(collectObject);
            currentCollector.AddInInventory(obj);

            StartGettnig();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollector coll))
        {
            currentCollector = null;
            gettingTimer?.Dispose();
        }
    }

    public void ChangeTime(float value)
    {
        currentTime = gettingTime / value;

        if (currentCollector != null)
        {
            StartGettnig();
        }
    }

    private void StartGettnig ()
    {
        gettingTimer?.Dispose();

        gettingTimer = Observable.Interval(TimeSpan.FromSeconds(currentTime)).TakeUntilDisable(gameObject).Subscribe(x =>
        {
            currentCollector.AddInInventory(Instantiate(collectObject));
        });
    }
}
