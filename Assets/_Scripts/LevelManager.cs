using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private int sceneBuildIndex;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _timer = 5.0f;
    private bool _isPlayerSafe;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isPlayerSafe)
        {
            _timer -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(_timer % 60);
            _timerText.text = seconds.ToString();
            if (_timer <= 0)
            {
                _timer = 5.0f;
                OnPlayerDeath();
            }
        }
        else
        {
            _timer = 5.0f;
            _timerText.text = "5";
        }
    }

    public void OnPlayerDeath()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OnLevelMove(Component sender, object data)
    {
        if (data is int)
        {
            sceneBuildIndex = (int)data;
        }
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    public void OnLightzone(Component sender, object data)
    {
        if (data is bool)
        {
            bool inLightzone = (bool)data;
            if (inLightzone)
            {
                _isPlayerSafe = true;
            }
            else
            {
                _isPlayerSafe = false;
            }
        }
    }
}