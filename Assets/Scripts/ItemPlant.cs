using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemPlant : MonoBehaviour
{
    public plantObject plant;
    public Image icon;
    public TMP_Text nameTxt;
    public TMP_Text priceTxt;
    public Image btnImage;
    public TMP_Text btnTxt;
    farmManage fm;
    // Start is called before the first frame update
    void Start()
    {
        fm = FindAnyObjectByType<farmManage>();
        InitializeUI();
    }
    public void BuyPlant()
    {
        Debug.Log("Bought" + plant.plantName);
        fm.SelectPlant(this);
    }
    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        priceTxt.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }


}
