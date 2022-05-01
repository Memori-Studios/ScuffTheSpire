using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TJ
{
public class Map : MonoBehaviour
{
    public List<Floor> floors;
	//public List<EncounterHeiarchy> allEncounters;
    public GameObject encounterPrefab;
    public Image enemyIcon, EliteIcon;
    public int eliteFloors;
    public int chestFloors;
    public int restFloors;
    public Encounter enemyEncounter;
    public Encounter eliteEncounter;
    public Encounter chestEncounter;
    public Encounter restEncounter;
    public int currentFloorNumber;
    private void Awake()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if(i==eliteFloors)
                floors[i].SetNodesActive(eliteEncounter);
            else if(i==chestFloors)
                floors[i].SetNodesActive(chestEncounter);
            else if(i==restFloors)
                floors[i].SetNodesActive(restEncounter);
            else
                floors[i].SetNodesActive(enemyEncounter);
        }
    }
    public void ShowOptions()
    {
        Debug.Log("showing options");
        if(currentFloorNumber==floors.Count)
        {
            currentFloorNumber=0;
            GenerateMap();
            return;
        }

        for (int i = 0; i < floors.Count; i++)
        {
            if(i==currentFloorNumber)
                floors[i].SetNodesActiveClickable();
        }
        currentFloorNumber++;
    }
    public void GenerateMap()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if(i==eliteFloors)
                floors[i].SetNodesActive(eliteEncounter);
            else if(i==chestFloors)
                floors[i].SetNodesActive(chestEncounter);
            else if(i==restFloors)
                floors[i].SetNodesActive(restEncounter);
            else
                floors[i].SetNodesActive(enemyEncounter);
        }
        ShowOptions();
    }
    public void ConnectFloors(Floor parentNodes, Floor childNodes)
    {
        //Debug.Log("we need to connect them");
        for (int i = 0; i < parentNodes.activeNodes.Count; i++)
        {
            if(childNodes.activeNodes.Contains(childNodes.nodes[0]))
            {
              
            }
        }
    }
    // public IEnumerator Delayedclicky()
    // {
    //     yield return new WaitForSeconds(1);
    //     ShowOptions();
    // }
    private void OnEnable()
    {
        ShowOptions();
    }
}

// public class EncounterHeiarchy:MonoBehaviour
// {
//     public List<Encounter> encounterParents;
//     public Encounter encounterSelf;
//     public List<Encounter> encounterChildren;
// }
[System.Serializable]
public struct Encounter
{
    public Type encounterType;
    public enum Type{enemy,elite,chest,rest};
    public Sprite encounterSprite;

}

}
