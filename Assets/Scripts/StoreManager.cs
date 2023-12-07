using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    List<plantObject> plantObjects = new List<plantObject>();   
    private void Awake()
    {
        var loadPlants = Resources.LoadAll("Plants", typeof(plantObject));
        foreach (var plant  in loadPlants)
        {
            plantObjects.Add((plantObject)plant);
            

        }
        plantObjects.Sort(SortByPrice);
        foreach (var plant in plantObjects)
        {
            ItemPlant newPlant = Instantiate(plantItem, transform).GetComponent<ItemPlant>();
            newPlant.plant = plant;
        }
    }
    int SortByPrice(plantObject plantObject1,  plantObject plantObject2)
    {
        return plantObject1.buyPrice.CompareTo(plantObject2.buyPrice);
    }
    int SortByTime(plantObject plantObject1, plantObject plantObject2)
    {
        return plantObject1.timeBtwStages.CompareTo(plantObject2.buyPrice);
    }
}
