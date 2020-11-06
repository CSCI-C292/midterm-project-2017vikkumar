using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class restart : MonoBehaviour
{
    public GameObject restartButton;

    public void RestartGame()
    {
        RaceStart.raceStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartButton.SetActive(false);

    }

}
