using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    public static void NewGame()
    {
        SceneManager.LoadScene("Farm", LoadSceneMode.Single);
    }
    public static void EndGame()
    {
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
    public static void Welcome()
    {
        SceneManager.LoadScene("Welcome", LoadSceneMode.Single);
    }
}
