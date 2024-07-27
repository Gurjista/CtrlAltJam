using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int sceneBuildIndex;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
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
}