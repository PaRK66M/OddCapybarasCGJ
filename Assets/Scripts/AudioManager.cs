using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource oldAudioSource;
    [SerializeField]
    AudioSource newAudioSource;

    [SerializeField]
    bool isInNewWorld;

    bool isInCoroutine;

    private void Start()
    {
        oldAudioSource.volume = !isInNewWorld ? PlayerPrefs.GetFloat("Volume") / 100.0f : 0.0f;
        newAudioSource.volume = isInNewWorld ? PlayerPrefs.GetFloat("Volume") / 100.0f : 0.0f;

        oldAudioSource.Play();
        newAudioSource.Play();
    }

    public void SwapWorld(float duration)
    {
        isInNewWorld = !isInNewWorld;
        isInCoroutine = true;
        StartCoroutine(SwappingWorlds(duration));
    }

    public void UpdateVolume()
    {
        if (isInCoroutine) { return; }

        oldAudioSource.volume = !isInNewWorld ? PlayerPrefs.GetFloat("Volume") / 100.0f : 0.0f;
        newAudioSource.volume = isInNewWorld ? PlayerPrefs.GetFloat("Volume") / 100.0f : 0.0f;
    }

    IEnumerator SwappingWorlds(float duration)
    {
        float currentTime = 0.0f;

        while(currentTime < duration / 0.5f)
        {
            float loudVolume = Mathf.Lerp(
                    PlayerPrefs.GetFloat("Volume") / 100.0f,
                    0.3f * PlayerPrefs.GetFloat("Volume") / 100.0f,
                    currentTime / (duration / 0.5f));

            float quietVolume = Mathf.Lerp(
                    0.0f,
                    0.3f * PlayerPrefs.GetFloat("Volume") / 100.0f,
                    currentTime / (duration / 0.5f));

            oldAudioSource.volume = isInNewWorld ?
                loudVolume :
                quietVolume;

            newAudioSource.volume = isInNewWorld ?
                quietVolume :
                loudVolume;

            currentTime += Time.deltaTime;
            yield return null;
        }

        while (currentTime < duration)
        {
            float loudVolume = Mathf.Lerp(
                    0.3f * PlayerPrefs.GetFloat("Volume") / 100.0f,
                    0.0f,
                    (currentTime - duration) / (duration / 0.5f));

            float quietVolume = Mathf.Lerp(
                    0.3f * PlayerPrefs.GetFloat("Volume") / 100.0f,
                    PlayerPrefs.GetFloat("Volume") / 100.0f,
                    (currentTime - duration) / (duration / 0.5f));

            oldAudioSource.volume = isInNewWorld ?
                loudVolume :
                quietVolume;

            newAudioSource.volume = isInNewWorld ?
                quietVolume :
                loudVolume;

            currentTime += Time.deltaTime;
            yield return null;
        }

        isInCoroutine = false;
    }
}
