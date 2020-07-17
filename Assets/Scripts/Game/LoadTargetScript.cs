using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadTargetScript : MonoBehaviour {

    public void LoadScreenNum(int num, int fromScene)
    {
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings) //błąd
        {
            Debug.LogWarning("Cant load scene num" + num + ", Scene manager only has " + SceneManager.sceneCountInBuildSettings + " scenes in BuildSettings!");
            return;
        }
            LoadingScreenManager.LoadScene(num, fromScene); //wywołanie skryptu umożliwiającego ładowanie sceny
    }
}

