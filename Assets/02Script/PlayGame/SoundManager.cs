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
        public static SoundManager instance;        //static���� �����Ͽ� �̱���� ���� ������ �ϵ��� �Ѵ�.
        private AudioSource[] sfx;              //AudioSource �����ʺz�� �����ϴ� �迭




        // ���۽� ������Ʈ�� ���ؼ� singleton���� �����Ѵ�.
        // ��𿡼��� SoundManager.PlayOnShot()���� ���带 �÷��� �� ���ִ�.
        void Start()
        {
            instance = GetComponent<SoundManager>();    //�� ���۽ÿ� SoundManager ������Ʈ�� ������ ���ؼ� static ����� ����
                                                        //��ġ�� ������� SoundManager.PlayOnShot(...)���� ���带 �÷��� �� �� �ִ�.
            sfx = GetComponents<AudioSource>();         //GameObject�� ��ϵ� ��� AudioSource ������Ʈ�� �迭�� ���ؼ� sfx����� ����
        }

        //���带 �÷����ϴ� �޼ҵ�
        public void PlayOneShot(Clip audioClip)
        {
            sfx[(int)audioClip].Play();     //�÷����� AudioSource�� �ε��� ��ȣ�� �ش�Ǵ� enum Ÿ��
        }

        //������ �������� AudioClip�� �÷����Ѵ�.

        public void PlayOneShot(Clip audioClip, float volumeScale)  //������ �������� ���带 �÷����ϴ� �޼ҵ�
        {
            AudioSource source = sfx[(int)audioClip];       //�÷����� AudioSouce�� �ε��� ��ȣ�� �ش�Ǵ� enumŸ��
            source.PlayOneShot(source.clip, volumeScale);
        }
    }
}