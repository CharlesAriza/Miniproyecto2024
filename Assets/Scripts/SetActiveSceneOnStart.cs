using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiveSceneOnStart : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }
}
