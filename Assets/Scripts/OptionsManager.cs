using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider mouseSensitivitySlider;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private AudioManager audioManager;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 50.0f);
            
        }
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");

        if (!PlayerPrefs.HasKey("MouseSensitivity"))
        {
            PlayerPrefs.SetFloat("MouseSensitivity", 50.0f);
        }
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");

        mouseSensitivitySlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    public void UpdateVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        if (audioManager != null)
        {
            audioManager.UpdateVolume();
        }
    }

    public void UpdateMouseSensitivity()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivitySlider.value);
        if (playerMovement != null)
        {
            playerMovement.UpdateMouseSensitivity();
        }
    }
}
