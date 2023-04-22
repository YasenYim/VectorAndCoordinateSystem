using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 坐标转换API : MonoBehaviour
{
    void Start()
    {
        Vector3 local = Vector3.forward;
        Vector3 world = Vector3.forward;

        // 转换位置，从局部到世界
        world = transform.TransformPoint(local);

        // 转换向量，从局部到世界。 TransformDirection不受缩放影响，TransformVector受缩放影响
        world = transform.TransformDirection(local);
        world = transform.TransformVector(local);



        // 转换位置，从世界到局部
        local = transform.InverseTransformPoint(world);

        // 转换向量，从世界到局部。 InverseTransformDirection不受缩放影响，InverseTransformVector受缩放影响
        local = transform.InverseTransformDirection(world);
        local = transform.InverseTransformVector(world);
    }

    void Update()
    {
        
    }
}
