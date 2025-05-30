using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    
[SerializeField] private GameObject _optionsPanel;

   
public void StartGameButton()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

public void OptionsButton()
{
    _optionsPanel.SetActive(true);
}

public void ExitButton()
{
    Application.Quit();
}
}
