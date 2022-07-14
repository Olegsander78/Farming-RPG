using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    private Crop _curCrop;
    public GameObject CropPrefab;

    public SpriteRenderer Sr;
    private bool _tilled;

    [Header("Sprites")]
    public Sprite GrassSprite;
    public Sprite TilledSprite;
    public Sprite WateredSprite;

    private void Start()
    {
        Sr.sprite = GrassSprite;
    }

    public void Interact()
    {
        if (!_tilled)
        {
            Till();
        }
        else if (!HasCrop() && GameManager.Instance.CanPlantCrop())
        {
            PlantNewCrop(GameManager.Instance.SelectedCropToPlant);
        }
        else if (HasCrop() && _curCrop.CanHarvest())
        {
            _curCrop.Harvest();
        }
        else
        {
            Water();
        }
    }

    private void PlantNewCrop(CropData crop)
    {
        if (!_tilled)
            return;

        _curCrop = Instantiate(CropPrefab, transform).GetComponent<Crop>();
        _curCrop.Plant(crop);

        GameManager.Instance.OnNewDay += OnNewDay;
    }

    private void Till()
    {
        _tilled = true;
        Sr.sprite = TilledSprite;
    }

    private void Water()
    {
        Sr.sprite = WateredSprite;
        if (HasCrop())
        {
            _curCrop.Water();
        }
    }

    private void OnNewDay()
    {
        if (_curCrop == null)
        {
            _tilled = false;
            Sr.sprite = GrassSprite;

            GameManager.Instance.OnNewDay -= OnNewDay;
        }
        else if (_curCrop != null)
        {
            Sr.sprite = TilledSprite;
            _curCrop.NewDayCheck();
        }

        
    }

    private bool HasCrop()
    {
        return _curCrop != null;
    }
}
