using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public float x, y, z;
    public Color _color = Color.red;
    

    private void OnDrawGizmos()
    {
        //x = this.transform.localScale.x;
        //y = this.transform.localScale.y;
        //z = this.transform.localScale.z;

        Gizmos.DrawWireCube(transform.position, new Vector3(x, y, z));
    }


}
