using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenuUI : MonoBehaviour
{

    [SerializeField] private GameObject _cursorCross;
    private PlayerInput _playerInput;
    private bool _isPaused = false;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private RectTransform _pausePanelRect;
    [SerializeField] private float _pausePanelTweenDuration;
    [SerializeField] private float topPosY, middlePosY;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        Resume();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !_optionsPanel.activeSelf)
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        _isPaused = true;
        Cursor.visible = true;
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _cursorCross.SetActive(false);
        PausePanelIntro();
#if UNITY_EDITOR
        
        Time.fixedDeltaTime = 0.0001f * Time.timeScale;
#endif
    }

    public async void Resume()
    {
        await PausePanelOutro();
        _isPaused = false;
        _pauseMenuUI.SetActive(false);
        _optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        _cursorCross.SetActive(true);
#if UNITY_EDITOR
        
        Time.fixedDeltaTime = 0.0001f;
#endif
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        Time.fixedDeltaTime = 0.0001f;
#endif
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenOptionsMenu()
    {
        _optionsPanel.SetActive(true);
       
    }

  private void PausePanelIntro()
  {
    _pausePanelRect.DOAnchorPosY(middlePosY, _pausePanelTweenDuration).SetUpdate(true);
  }

  async Task PausePanelOutro()
  {
    await _pausePanelRect.DOAnchorPosY(topPosY, _pausePanelTweenDuration).SetUpdate(true).AsyncWaitForCompletion();
  }
}