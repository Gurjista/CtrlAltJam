using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    private InputAction _lightAction;
    private bool _isTryingLight;

    private Light2D _playerLight;
    private float _startingOuterLightRadius;
    private float _startingInnerLightRadius;
    private float _startingIntensity;
    private float _startingFalloff;

    [SerializeField] private float _largeLightRadius;
    [SerializeField] private float _smallLightRadius;
    [SerializeField] private float _largeIntensity;
    [SerializeField] private float _largeFalloff;

    private bool _isEnergyDepleted = false;
    private bool _inLightzone = false;
    private bool _previousInLightzone = false;

    [Header("Events")]
    public GameEvent onLightOn;

    public GameEvent OnLightzone;

    [SerializeField] private float _energyCost;

    private void Awake()
    {
        //_playerMovement = GetComponent<PlayerMovement>();
        _playerLight = GetComponentInChildren<Light2D>();
    }

    private void Start()
    {
        _startingOuterLightRadius = _playerLight.pointLightOuterRadius;
        _startingInnerLightRadius = _playerLight.pointLightInnerRadius;
        _startingIntensity = _playerLight.intensity;
        _startingFalloff = _playerLight.falloffIntensity;
    }

    #region Callbacks

    private void OnEnable()
    {
        _lightAction = InputManager.Instance._inputs.PlayerInput.Light;
        _lightAction.performed += OnLight;
        _lightAction.canceled += OnLight;
    }

    private void OnDisable()
    {
        _lightAction.performed -= OnLight;
        _lightAction.canceled -= OnLight;
    }

    public void OnLight(InputAction.CallbackContext ctx)
    {
        _isTryingLight = ctx.ReadValue<float>() > 0;
    }

    #endregion Callbacks

    // Update is called once per frame
    private void Update()
    {
        UpdateAcender();
    }

    private void UpdateAcender()
    {
        if (_isEnergyDepleted)
        {
            _isTryingLight = false;
        }
        var largeLightTarget = _isTryingLight ? _largeLightRadius : _startingOuterLightRadius;
        var smallLightTarget = _isTryingLight ? _smallLightRadius : _startingInnerLightRadius;
        var intensityTarget = _isTryingLight ? _largeIntensity : _startingIntensity;
        var falloffTarget = _isTryingLight ? _largeFalloff : _startingFalloff;
        _inLightzone = _isTryingLight ? true : false;
        if (_isTryingLight)
            onLightOn.Raise(this, _energyCost);
        if (_inLightzone != _previousInLightzone)
        {
            OnLightzone.Raise(this, _inLightzone);
            _previousInLightzone = _inLightzone;
        }

        _playerLight.pointLightOuterRadius = largeLightTarget;
        _playerLight.pointLightInnerRadius = smallLightTarget;
        _playerLight.intensity = intensityTarget;
        _playerLight.falloffIntensity = falloffTarget;
    }

    public void OnEnergyDepleted(Component sende, object data)
    {
        _isEnergyDepleted = (bool)data;
    }
}