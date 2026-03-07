using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    GameObject lossScreen;

    bool isGameLost = false;

    [SerializeField]
    float duration;

    [SerializeField]
    Image _blackScreen;

    public void OnGameLoss()
    {
        if (isGameLost)
        {
            return;
        }

        isGameLost = true;

        Debug.Log("Game Is Lost");

        playerObject.GetComponent<PlayerInput>().DisableInput();

        lossScreen.SetActive(true);

        StartCoroutine(ChangeBlackScreenAlpha(0.0f, 1.0f, duration));
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

        ReloadLevel();
    }

    void ReloadLevel()
    {

    }
}
