using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro���g�p���邽�ߒǉ�

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Toggle bgmToggle;
    // FPS�ݒ�p�̃h���b�v�_�E����ǉ�
    public TMP_Dropdown fpsDropdown;

    void Start()
    {
        // ������Ԃł͐ݒ�p�l�����\���ɂ���
        settingsPanel.SetActive(false);

        // �X���C�_�[�ƃg�O���̏����l��ݒ�
        if (volumeSlider != null)
        {
            volumeSlider.value = 0.5f; // ��: 0.5�ɏ����ݒ�
        }
        if (bgmToggle != null)
        {
            bgmToggle.isOn = true; // ��: ON�ɏ����ݒ�
        }

        // �h���b�v�_�E���̏����ݒ�
        if (fpsDropdown != null)
        {
            // �I�v�V�������N���A
            fpsDropdown.ClearOptions();
            // �I�v�V������ǉ�
            fpsDropdown.AddOptions(new System.Collections.Generic.List<string> { "30", "50", "60" });
            // �h���b�v�_�E���̏����l�����݂�FPS�ɐݒ�
            fpsDropdown.value = fpsDropdown.options.FindIndex(option => option.text == Application.targetFrameRate.ToString());
            // �h���b�v�_�E���̒l���ύX���ꂽ�Ƃ��̃��X�i�[��ǉ�
            fpsDropdown.onValueChanged.AddListener(OnFPSDropdownChanged);
        }

        // ����FPS��ݒ�
        SetFPS(30);
    }

    // �ݒ�p�l���̕\��/��\����؂�ւ��郁�\�b�h
    public void ToggleSettingsPanel()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
    }

    // ���ʃX���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void OnVolumeChanged(float value)
    {
        Debug.Log("����: " + value);
        // �����Ŏ��ۂ̃I�[�f�B�I�\�[�X�̉��ʂ�ύX���鏈�����L�q
        // ��: AudioListener.volume = value;
    }

    // BGM�g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void OnBGM_ToggleChanged(bool isOn)
    {
        Debug.Log("BGM ON/OFF: " + isOn);
        // ������BGM�̍Đ�/��~��؂�ւ��鏈�����L�q
    }

    // �h���b�v�_�E���̒l���ύX���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void OnFPSDropdownChanged(int index)
    {
        int fps = int.Parse(fpsDropdown.options[index].text);
        SetFPS(fps);
    }

    // FPS��ݒ肷�郁�\�b�h
    private void SetFPS(int fps)
    {
        Application.targetFrameRate = fps;
        Debug.Log("FPS: " + fps);
    }
}