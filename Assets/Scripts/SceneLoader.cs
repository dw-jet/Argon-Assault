using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadLevel", 5f);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
