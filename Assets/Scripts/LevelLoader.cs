using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
