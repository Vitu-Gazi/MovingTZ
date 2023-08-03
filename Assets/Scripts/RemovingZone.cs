using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RemovingZone : MonoBehaviour
{
    [SerializeField] private float removingTime = 1;

    private float currentTime;

    private IDisposable removingTimer;
    private ICollector currentCollector;

    private void Start()
    {
        currentTime = removingTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out currentCollector))
        {
            currentCollector.RemoveLastInInventory();

            StartRemoving();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollector coll))
        {
            removingTimer?.Dispose();
            currentCollector = null;
        }
    }

    public void ChangeTime(float newValue)
    {
        currentTime = removingTime / newValue;

        if (currentCollector != null)
        {
            StartRemoving();
        }
    }


    private void StartRemoving()
    {
        removingTimer?.Dispose();

        removingTimer = Observable.Interval(TimeSpan.FromSeconds(currentTime)).TakeUntilDisable(gameObject).Subscribe(x =>
        {
            currentCollector.RemoveLastInInventory();
        });
    }
}
