using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip[] soundEffects; // 효과음 클립 배열
    private AudioSource audioSource;  // AudioSource 컴포넌트

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 마우스 좌클릭 확인
        if (Input.GetMouseButtonDown(0)) 
        {
            PlayRandomSound();  // 랜덤 효과음 재생
        }
    }

    // 랜덤 효과음 재생
    void PlayRandomSound()
    {
        if (soundEffects.Length > 0)
        {
            // 배열에서 랜덤으로 효과음 선택
            int randomIndex = Random.Range(0, soundEffects.Length);
            audioSource.clip = soundEffects[randomIndex];
            audioSource.Play();  // 선택된 효과음 재생
        }
    }
}
