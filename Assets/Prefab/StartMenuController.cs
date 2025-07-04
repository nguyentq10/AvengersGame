using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // Thay "SampleScene" bằng tên chính xác của scene bạn muốn load
        SceneManager.LoadScene("SampleScene");
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Open Settings Menu");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}
