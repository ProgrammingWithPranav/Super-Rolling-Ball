using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScrrenFromControlls : MonoBehaviour
{
    public void SwitchScreenFromControlls()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
