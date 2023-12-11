using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class farmManage : MonoBehaviour
{
    public bool isPlanting = false;
    public ItemPlant selectPlant;
    public int money = 100;
    public TMP_Text moneyTxt;
    public Color buyColor = Color.green;
    public Color CancelColor = Color.red;
    public bool isSelecting = false;
    public int selectedTool = 0;

    public Image[] buttonsImg = new Image[4]; // Resize the array to hold four buttons
    public Sprite normalButton;
    public Sprite selectedButton;

    void Start()
    {
        moneyTxt.text = "$" + money;
    }

    public void SelectPlant(ItemPlant newPlant)
    {
        if (selectPlant == newPlant)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();
            selectPlant = newPlant;
            selectPlant.btnImage.color = CancelColor;
            selectPlant.btnTxt.text = "Cancel";
            isPlanting = true;
        }
    }

    public void SelectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber - 1].sprite = selectedButton; // Assuming toolNumber starts from 1
        }
    }

    void CheckSelection()
    {
        if (isPlanting)
        {
            isPlanting = false;
            if (selectPlant != null)
            {
                selectPlant.btnImage.color = buyColor;
                selectPlant.btnTxt.text = "Buy";
                selectPlant = null;
            }
        }
        if (isSelecting)
        {
            if (selectedTool > 0 && selectedTool <= buttonsImg.Length) // Check the array bounds
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int value)
    {
        money += value;
        moneyTxt.text = "$" + money;
    }
}
