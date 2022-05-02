using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{

    public enum Clip
    {
        Chomp = 0,
        BlcokClear = 1
    };

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;        //static으로 선언하여 싱글톤과 같은 역할을 하도록 한다.
        private AudioSource[] sfx;              //AudioSource 컴포너틑를 참조하는 배열




        // 시작시 컴포넌트를 구해서 singleton으로 저장한다.
        // 어디에서나 SoundManager.PlayOnShot()으로 사운드를 플레이 할 수있다.
        void Start()
        {
            instance = GetComponent<SoundManager>();    //씬 시작시에 SoundManager 컴포넌트의 참조를 구해서 static 멤버에 저장
                                                        //위치에 관계없이 SoundManager.PlayOnShot(...)으로 사운드를 플레이 할 수 있다.
            sfx = GetComponents<AudioSource>();         //GameObject에 등록된 모든 AudioSource 컴포넌트의 배열을 구해서 sfx멤버가 참조
        }

        //사운드를 플레이하는 메소드
        public void PlayOneShot(Clip audioClip)
        {
            sfx[(int)audioClip].Play();     //플레이할 AudioSource의 인덱스 번호에 해당되는 enum 타입
        }

        //지점된 볼륨으로 AudioClip을 플레이한다.

        public void PlayOneShot(Clip audioClip, float volumeScale)  //지정된 볼륨으로 사운드를 플레이하는 메소드
        {
            AudioSource source = sfx[(int)audioClip];       //플레이할 AudioSouce의 인덱스 번호에 해당되는 enum타입
            source.PlayOneShot(source.clip, volumeScale);
        }
    }
}