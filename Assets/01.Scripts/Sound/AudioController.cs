using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;   

public class AudioController : MonoBehaviour
{
    [Header("Mixer & UI")] 

    public AudioMixer audioMixer; // 연결할 AudioMixer (예: MainAudioMixer)
    public Slider volumeSlider;   // 연결할 볼륨 조절용 슬라이더
    public Toggle muteToggle;     // 연결할 음소거용 토글

    private const string exposedParam = "BGMVolume"; // AudioMixer에서 노출한 파라미터 이름
    private float previousVolume = 1f; // 음소거 이전의 마지막 볼륨 값 저장용

    void Start()
    {
        // 슬라이더가 존재하면 초기 설정을 한다
        if (volumeSlider != null)
        {
            // 저장된 볼륨 값 불러오기 (없으면 기본값 1)
            float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);

            // 슬라이더 초기값 설정
            volumeSlider.value = savedVolume;

            // 볼륨 적용
            SetVolume(savedVolume);

            // 슬라이더 값이 바뀔 때마다 SetVolume 함수 호출
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // 토글이 존재하면 이벤트 리스너 등록
        if (muteToggle != null)
        {
            // 토글이 변경될 때마다 SetMute 함수 호출
            muteToggle.onValueChanged.AddListener(SetMute);
        }
    }

    // 볼륨 조절 함수
    public void SetVolume(float volume)
    {
        // dB 단위로 변환 (0~1 값을 -80 ~ 0 dB로 바꿔서 Mixer에 적용)
        // 볼륨 0이면 log 연산 불가하므로 최소 0.0001로 제한
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;

        // AudioMixer에 볼륨 값 적용
        audioMixer.SetFloat(exposedParam, dB);

        // 이전 볼륨값 기억 (음소거 해제 시 복원용)
        previousVolume = volume;

        // 현재 볼륨값 저장 (재실행 시 유지)
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    // 음소거 토글 함수
    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            // 음소거 시, AudioMixer 볼륨을 최저값으로 설정 (-80 dB)
            audioMixer.SetFloat(exposedParam, -80f);
        }
        else
        {
            // 음소거 해제 시, 슬라이더 값을 기준으로 다시 볼륨 설정
            SetVolume(volumeSlider.value);
        }
    }
}
