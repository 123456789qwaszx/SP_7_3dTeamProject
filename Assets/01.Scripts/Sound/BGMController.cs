using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;   

public class BGMController : MonoBehaviour
{
    [Header("Mixer & UI")] 

    public AudioMixer bgmAudioMixer; // 연결할 AudioMixer (예: MainAudioMixer)
    public Slider bgmSlider;   // 연결할 볼륨 조절용 슬라이더
    public Toggle bgmMuteToggle;     // 연결할 음소거용 토글

    private const string bgmParam = "BGMVolume"; // AudioMixer에서 노출한 파라미터 이름
    private float bgmPreviousVolume = 1f; // 음소거 이전의 마지막 볼륨 값 저장용

    void Start()
    {
        PlayerPrefs.SetFloat(bgmParam, 1f); // 강제 초기화
        PlayerPrefs.Save();
        if (bgmSlider != null)// 슬라이더가 존재하면 초기 설정을 한다
        {
            float savedBgm = PlayerPrefs.GetFloat(bgmParam, 1f);// 저장된 볼륨 값 불러오기 (없으면 기본값 1)
            bgmSlider.value = savedBgm;   // 슬라이더 초기값 설정
            bgmSetVolume(savedBgm);  // 볼륨 적용
            bgmSlider.onValueChanged.AddListener(bgmSetVolume);// 슬라이더 값이 바뀔 때마다 SetVolume 함수 호출
        }

       
        if (bgmMuteToggle != null) // 토글이 존재하면 이벤트 리스너 등록
        {
            bgmMuteToggle.onValueChanged.AddListener(bgmSetMute);// 토글이 변경될 때마다 SetMute 함수 호출
        }
    }

    
    public void bgmSetVolume(float volume)// 볼륨 조절 함수
    {
        // dB 단위로 변환 (0~1 값을 -80 ~ 0 dB로 바꿔서 Mixer에 적용)
        // 볼륨 0이면 log 연산 불가하므로 최소 0.0001로 제한
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        bgmAudioMixer.SetFloat(bgmParam, dB); // AudioMixer에 볼륨 값 적용
        bgmPreviousVolume = volume;// 이전 볼륨값 기억 (음소거 해제 시 복원용)
        PlayerPrefs.SetFloat(bgmParam, volume); // 현재 볼륨값 저장 (재실행 시 유지)
        if (bgmMuteToggle != null && bgmMuteToggle.isOn)
        {
            bgmMuteToggle.isOn = false;
        }
    }

    
    public void bgmSetMute(bool isMuted)// 음소거 토글 함수
    {
        if (isMuted)
        {
            bgmAudioMixer.SetFloat(bgmParam, -80f);// 음소거 시, AudioMixer 볼륨을 최저값으로 설정 (-80 dB)
        }
        else
        {
            bgmSetVolume(bgmSlider.value);// 음소거 해제 시, 슬라이더 값을 기준으로 다시 볼륨 설정
        }
    }
}
