using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int CurDay;
    public int Money;
    public int CropInventory;

    public CropData SelectedCropToPlant;
    public TextMeshProUGUI StatsText;

    public event UnityAction OnNewDay;


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetNextDay()
    {
        CurDay++;
        OnNewDay?.Invoke();
        UpdateStatsText();
    }

    public void OnPlantCrop(CropData crop)
    {
        CropInventory--;
        UpdateStatsText();
    }

    public void OnHarvestCrop(CropData crop)
    {
        Money += crop.SellPrice;
        UpdateStatsText();
    }

    public void PurchaseCrop(CropData crop)
    {
        Money -= crop.PurchasePrice;
        CropInventory++;
        UpdateStatsText();
    }

    public bool CanPlantCrop()
    {
        return CropInventory > 0;
    }

    public void OnBuyCropButton(CropData crop)
    {
        if (Money > crop.PurchasePrice)
        {
            PurchaseCrop(crop);
        }
    }

    private void UpdateStatsText()
    {
        StatsText.text = $"Day: {CurDay}\nMoney: ${Money}\nCrop Inventory: {CropInventory}";
    }

    private void OnEnable()
    {
        Crop.OnPlantCrop += OnPlantCrop;
        Crop.OnHarvestCrop += OnHarvestCrop;
    }

    private void OnDisable()
    {
        Crop.OnPlantCrop -= OnPlantCrop;
        Crop.OnHarvestCrop -= OnHarvestCrop;
    }
}
