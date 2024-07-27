using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Inputs _inputs;

    private static InputManager _instance;
    public static InputManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _inputs = new Inputs();
            _inputs.Enable();
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}