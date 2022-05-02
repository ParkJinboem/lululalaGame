using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Effect
{
    //[RequireComponent] Attribute�� ������Ʈ�� ��ϵ� �� ������ ������Ʈ�� ������ ��ϵǾ��ִ��� üũ�Ѵ�.
    //ParticleSystem�� ������Ʈ�� ��ϵǾ� �־�� �Ѵ�.

    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleAutoDestroy : MonoBehaviour
    {
        void OnEnable()
        {
            StartCoroutine(CoCheckAlive());
        }

        IEnumerator CoCheckAlive()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (!GetComponent<ParticleSystem>().IsAlive(true))
                {
                    Destroy(this.gameObject);

                    break;
                }
            }
        }
    }
}