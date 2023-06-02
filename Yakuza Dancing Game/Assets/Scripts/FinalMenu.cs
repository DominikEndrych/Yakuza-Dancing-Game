using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreCounter;
    [SerializeField] TextMeshProUGUI _finalScore;

    // Set text for final score
    public void SetFinalScore()
    {
        _finalScore.text = _scoreCounter.text;
    }

    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
