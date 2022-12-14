using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public string launchSceneName;
    public string returnSceneName;

    public void ChangeDifficulty(int diff)
    {
        GameManager.instance.SetDifficulty((Difficulty)diff);
    }

    public void ChangeCategorie(int cat)
    {
        GameManager.instance.SetCategorie((Categorie)cat);
    }

    public void LaunchGame()
    {
        GameManager.instance.ChangeScene(launchSceneName);
    }

    public void Return()
    {
        GameManager.instance.ChangeScene(returnSceneName);
    }
}
