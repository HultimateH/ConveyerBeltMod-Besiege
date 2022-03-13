using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;

class StableAxisBlockScript : BlockScript
{
    [SerializeField]
    GameObject go;
    public override void OnBlockPlaced()
    {
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(0f, 0f, 2f);
        go.AddComponent<Rigidbody>().isKinematic = true;
        var cj =go.AddComponent<ConfigurableJoint>();
        cj.connectedBody = Rigidbody;
        cj.xMotion = cj.yMotion = cj.zMotion = ConfigurableJointMotion.Locked;
        /*cj.angularXMotion =*/ cj.angularYMotion = cj.angularZMotion = ConfigurableJointMotion.Locked;
        cj.axis = Vector3.forward;


        var point = transform.FindChild("Adding Point");
        point.SetParent(go.transform);
        point.localPosition = new Vector3(0f, 0f, -0.2f);
    }


    public override void SimulateFixedUpdateAlways()
    {

        var axis = transform.TransformDirection(transform.InverseTransformDirection(Vector3.forward));
        Quaternion angle1 = Quaternion.AngleAxis(1 * 3.14f * 50f * Time.fixedDeltaTime, axis * (BlockBehaviour.Flipped ? 1f : -1f));
        var rigid = go.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.constraints = RigidbodyConstraints.FreezeRotationZ;
        rigid.WakeUp();
        rigid.MoveRotation(go.transform.rotation * angle1);
    }
}

