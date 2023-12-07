using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plotmanagement : MonoBehaviour
{

    bool isPlanted = false;
    SpriteRenderer plant;
    int plantStage = 0;
    BoxCollider2D plantCollider;
    float timer;
    plantObject selectedPlant;
    farmManage fm;
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;
    SpriteRenderer plot;
    bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavailableSprite;
    float speed = 1f;
    public bool isBought = true;
    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.parent.GetComponent<farmManage>();
        plot = GetComponent<SpriteRenderer>();
        if (isBought)
        {
            plot.sprite = drySprite;
        }
        else
        {
            
            plot.sprite = unavailableSprite;
        }
     

    }

    // Update is called once per frame
    void Update()
    {
       if(isPlanted && !isDry)
        {
            timer -= speed*Time.deltaTime;
            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
            {
            if (isPlanted)
            {
                if (plantStage == selectedPlant.plantStages.Length - 1 && !fm.isPlanting && !fm.isSelecting)
                {
                    Harvest();
                    
                }
            }
            else if (fm.isPlanting && fm.selectPlant.plant.buyPrice <= fm.money)
            {
                Plant(fm.selectPlant.plant);
            }
            
        }

        if (fm.isPlanting)
        {
            if (isPlanted || fm.selectPlant.plant.buyPrice > fm.money)
            {
                plot.color = unavailableColor;
            }
            else
            {
                plot.color = availableColor;
            }
        }
        if (fm.isSelecting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                case 2:
                    if (isBought && fm.money >= (fm.selectedTool - 1) * 10)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                case 3:
                    if (!isBought && fm.money >= 100)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }
    private void OnMouseDown()
    {
        if (fm.isSelecting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                    if (isBought)
                    {
                        isDry = false;
                        plot.sprite = normalSprite;
                        if (isPlanted) UpdatePlant();
                    }
                   
                    break;
                case 2:
                    if (fm.money >= 5 && isBought)
                    {
                        fm.Transaction(-5);
                        if (speed < 2) speed += .2f;
                    }
                    break;
                case 3:
                    if(fm.money >= 100 && !isBought)
                    {
                        fm.Transaction(-100);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private void OnMouseExit()
    {
        plot.color = Color.white;
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;
    }
    void Plant(plantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;
        fm.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        if(isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }

}
