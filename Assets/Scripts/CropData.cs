using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop Data", menuName = "New Crop Data")]

public class CropData : ScriptableObject
{
    public int DaysToGrow;
    public Sprite[] GrowProgressSprites;
    public Sprite ReadyToHarvestSprites;

    public int PurchasePrice;
    public int SellPrice;
}
