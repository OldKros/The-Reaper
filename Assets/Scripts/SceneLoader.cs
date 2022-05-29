using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int sceneIndexToLoadOnLoad = 0;

    private void OnEnable()
    {
        SceneManager.LoadScene(sceneIndexToLoadOnLoad);
    }
}
