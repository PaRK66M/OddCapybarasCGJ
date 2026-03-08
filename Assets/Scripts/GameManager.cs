using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    GameObject lossScreen;
    [SerializeField]
    GameObject victoryScreen;

    bool isGameFinished = false;

    [SerializeField]
    float duration;

    [SerializeField]
    Image _blackScreen;

    [SerializeField]
    private GameObject pauseMenu;
    private bool isPaused;

    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private LevelLoader levelLoader;


    public void OnGameLoss()
    {
        if (isGameFinished)
        {
            return;
        }

        isGameFinished = true;

        playerObject.GetComponent<PlayerInput>().DisableInput();

        lossScreen.SetActive(true);

        StartCoroutine(ChangeBlackScreenAlpha(0.0f, 1.0f, duration));
        Invoke("ReloadLevel", duration);
    }

    public void OnGameWin()
    {
        if (isGameFinished)
        {
            return;
        }

        isGameFinished = true;

        playerObject.GetComponent<PlayerInput>().DisableInput();

        victoryScreen.SetActive(true);

        StartCoroutine(ChangeBlackScreenAlpha(0.0f, 1.0f, duration));

        Invoke("ReturnToMenu", duration);
    }

    public IEnumerator ChangeBlackScreenAlpha(float start, float end, float time)
    {
        float timePassed = 0.0f;
        Color screenColour = _blackScreen.color;

        while (timePassed <= time)
        {
            screenColour.a = Mathf.Lerp(start, end, timePassed / time);
            _blackScreen.color = screenColour;

            timePassed += Time.deltaTime;
            yield return null;
        }

        screenColour.a = end;
        _blackScreen.color = screenColour;
    }

    void ReloadLevel()
    {
        levelLoader.LoadLevel(1);
    }

    void ReturnToMenu()
    {
        levelLoader.LoadLevel(0);
    }

    public void PauseGameInput(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        PauseGame();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        Cursor.lockState = isPaused ?
            CursorLockMode.None :
            CursorLockMode.Locked;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            playerInput.DisableInput(true);
        }
        else
        {
            playerInput.EnableInput(true);
        }
    }
}
