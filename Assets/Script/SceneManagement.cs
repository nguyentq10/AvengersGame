using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1"); // Thay bằng tên scene chơi chính
    }

}
