using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMenu : MonoBehaviour
{
    private void Awake() 
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicMenu");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}
