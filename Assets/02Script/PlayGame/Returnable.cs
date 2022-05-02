using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class Returnable<T>
    {
        public T value { get; set; }

        public Returnable(T value)
        {
            this.value = value;
        }
    }
}
