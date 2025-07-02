using UnityEngine;

public class ScSceneManagement : MonoBehaviour 
{
    public void LoadScene(string sceneName) 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
