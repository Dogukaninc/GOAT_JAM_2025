using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _cursorCross;
    private bool _isPaused = false;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private RectTransform _pausePanelRect;
    [SerializeField] private float _pausePanelTweenDuration;
    [SerializeField] private float topPosY, middlePosY;
    [SerializeField] private GameObject _playerStatsUI;
    [SerializeField] private float healthPosY, bottomPosY;
    [SerializeField] private RectTransform _healthRect;
    [SerializeField] private RectTransform _playerDeadUIRect;
    [SerializeField] private GameObject playerDeadUI;
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        PausePanelIntro();
        ClosePlayerStats();
    }

    public void Resume()
    {
        PausePanelOutro();
        OpenPlayerStats();
        Time.timeScale = 1f;
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenOptionsMenu()
    {
        _optionsPanel.SetActive(true);
    }

    private void PausePanelIntro()
    {
        _pauseMenuUI.SetActive(true);
        _cursorCross.SetActive(false);
        _isPaused = true;
        Cursor.visible = true;
        _pausePanelRect.DOAnchorPosY(middlePosY, _pausePanelTweenDuration).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
    }

    public void PausePanelOutro()
    {
        Time.timeScale = 1;
        _pausePanelRect.DOAnchorPosY(topPosY, _pausePanelTweenDuration).OnComplete(() =>
        {
            _isPaused = false;
            _pauseMenuUI.SetActive(false);
            _optionsPanel.SetActive(false);
            Cursor.visible = false;
            _cursorCross.SetActive(true);
        });
    }

    public void OpenPlayerStats()
    {
         _healthRect.DOAnchorPosY(healthPosY, _pausePanelTweenDuration);
        _playerStatsUI.SetActive(true);
    }

    public void ClosePlayerStats()
    {
        _healthRect.DOAnchorPosY(bottomPosY, _pausePanelTweenDuration).OnComplete(() =>
        {
            _playerStatsUI.SetActive(false);
        });
    }


public void OpenPlayerDeadUI(Component sender, object data)
{
    Debug.Log("OpenPlayerDeadUI");
    playerDeadUI.SetActive(true);
    _cursorCross.SetActive(false);
    _isPaused = true;
    Cursor.visible = true;
    _playerDeadUIRect.DOAnchorPosY(middlePosY, _pausePanelTweenDuration);
}



}