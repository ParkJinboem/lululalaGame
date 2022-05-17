using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class Returnable<T>  //코루틴의 결과를 수신하기 위한 범용 클래스
    {
        public T value { get; set; }

        public Returnable(T value)
        {
            this.value = value;
        }
    }
}
