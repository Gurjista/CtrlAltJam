using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private RectTransform _barRect;

    [SerializeField] private RectMask2D _mask;

    private float _maxRightMask;
    private float _initialRightMask;

    private float _maxEnergy = 200;
    private float _currentEnergy;

    [Header("Events")]
    public GameEvent OnEnergyDepleted;

    private void Start()
    {
        // x = left, w = top, y = bottom, z = right
        _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
        _initialRightMask = _mask.padding.z;
        _currentEnergy = _maxEnergy;
    }

    public void UpdateEnergy(Component sender, object data)
    {
        if (data is float)
        {
            float amount = (float)data;
            float targetWidth;
            if (sender is PlayerLight)
            {
                _currentEnergy -= amount;
                if (_currentEnergy < 0)
                {
                    _currentEnergy = 0;
                    OnEnergyDepleted.Raise(this, true);
                }
            }
            else if (sender is Pilha)
            {
                if (_currentEnergy == 0)
                {
                    OnEnergyDepleted.Raise(this, false);
                }
                _currentEnergy += amount;
                if (_currentEnergy > _maxEnergy)
                {
                    _currentEnergy = _maxEnergy;
                }
            }
            targetWidth = _currentEnergy * _maxRightMask / _maxEnergy;
            SetEnergy(targetWidth);
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