using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // [SerializeField] private Energy energy;
    [SerializeField] private RectTransform _barRect;

    [SerializeField] private RectMask2D _mask;

    private float _maxRightMask;
    private float _initialRightMask;

    private float _maxEnergy = 100;
    //private float _currentEnergy = 100;

    private void Start()
    {
        // x = left, w = top, y = botto, z = right
        _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
        _initialRightMask = _mask.padding.z;
    }

    public void UpdateEnergy(Component sender, object data)
    {
        if (data is float)
        {
            Debug.LogWarning("Spending energy");
            float amount = (float)data;
            var targetWidth = amount * _maxRightMask / _maxEnergy;
        }
    }

    private void SetEnergy(float targetWidth)
    {
        var newRightMask = _maxRightMask + _initialRightMask - targetWidth;
        var padding = _mask.padding;
        padding.z = newRightMask;
        _mask.padding = padding;
    }
}