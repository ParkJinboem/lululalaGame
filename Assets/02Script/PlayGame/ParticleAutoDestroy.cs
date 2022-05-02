using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Effect
{
    //[RequireComponent] Attribute는 컴포넌트로 등록될 때 지정한 컴포넌트가 사전에 등록되어있는지 체크한다.
    //ParticleSystem이 컴포넌트로 등록되어 있어야 한다.

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