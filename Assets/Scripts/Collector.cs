using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour, ICollector
{
    [SerializeField] private Transform inventoryPoint;
    [SerializeField] private int stack = 6;

    private List<GameObject> inventory = new List<GameObject>();
    private int currentStacks = 0;

    public List<GameObject> GetInventory()
    {
        return inventory;
    }

    public void AddInInventory(GameObject newObj)
    {
        inventory.Add(newObj);
        SetPosition(newObj);
        UpdateStacks();
    }

    public void RemoveLastInInventory()
    {
        if (inventory.Count == 0)
        {
            return;
        }

        GameObject removingObj = inventory.Last();
        inventory.Remove(removingObj);
        Destroy(removingObj);

        UpdateStacks();
    }

    private void SetPosition (GameObject newObj)
    {
        newObj.transform.parent = inventoryPoint.transform;

        Vector3 newPos = Vector3.zero;

        newPos.x = newObj.transform.lossyScale.x * currentStacks;
        newPos.y = (((float)inventory.Count / stack) - currentStacks) * (newObj.transform.localScale.y * 10);

        newObj.transform.localPosition = newPos;
    }

    private void UpdateStacks()
    {
        currentStacks = inventory.Count / stack;
    }
}

public interface ICollector
{
    public List<GameObject> GetInventory();
    public void AddInInventory(GameObject newObj);
    public void RemoveLastInInventory();
}
