using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScreenToControlls : MonoBehaviour
{
    public void SwitchScreenToControlls()
    {
        SceneManager.LoadScene("ControlsScene");
    }
}
