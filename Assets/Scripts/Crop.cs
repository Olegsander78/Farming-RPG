using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    private CropData _curCrop;
    private int _plantDay;
    private int _daysSinceLastWatered;

    public SpriteRenderer Sr;

    public static event UnityAction<CropData> OnPlantCrop;
    public static event UnityAction<CropData> OnHarvestCrop;

    public void Plant(CropData crop)
    {
        _curCrop = crop;
        _plantDay = GameManager.Instance.CurDay;
        _daysSinceLastWatered = 1;
        UpdateCropSprite();

        OnPlantCrop?.Invoke(crop);
    }

    public void NewDayCheck()
    {
        _daysSinceLastWatered++;
        
        if (_daysSinceLastWatered > 3)
        {
            Destroy(gameObject);
        }
        UpdateCropSprite();
    }

    private void UpdateCropSprite()
    {
        int cropProg = CropProgress();
        if(cropProg < _curCrop.DaysToGrow)
        {
            Sr.sprite = _curCrop.GrowProgressSprites[cropProg];
        }
        else
        {
            Sr.sprite = _curCrop.ReadyToHarvestSprites;
        }

    }

    public void Water()
    {
        _daysSinceLastWatered = 0;
    }

    public void Harvest()
    {
        if (CanHarvest())
        {
            OnHarvestCrop?.Invoke(_curCrop);
            Destroy(gameObject);
        } 
    }

    private int CropProgress()
    {
        return GameManager.Instance.CurDay - _plantDay;
    }

    public bool CanHarvest()
    {
        return CropProgress() >= _curCrop.DaysToGrow;
    }
}
