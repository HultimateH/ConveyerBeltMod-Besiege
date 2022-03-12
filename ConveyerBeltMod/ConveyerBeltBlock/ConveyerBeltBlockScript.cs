using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;

class ConveyerBeltBlockScript : BlockScript
{
    MSlider speedSlider,frictionSlider;
    
    [SerializeField]
    private GameObject plain1, plain2;
    [SerializeField]
    private GameObject cylinder1, cylinder2;

    public override void SafeAwake()
    {
        speedSlider = AddSlider("speed", "speed", 1f, 0f, 5f);


    }

    public override void OnBlockPlaced()
    {
        plain1 = createPlain("Plain 1", new Vector3(0f, 0.2f, 0.75f));
        plain2 = createPlain("Plain 2", new Vector3(0f, -0.2f, 0.75f));
        cylinder1 = createCylinder("Cylinder 1", new Vector3(1.25f, 0f, 0.75f));
        cylinder2 = createCylinder("Cylinder 2", new Vector3(-1.25f, 0f, 0.75f));

        GameObject createPlain(string name,Vector3 localPosition)
        {
            var go = new GameObject(name);
            go.AddComponent<MeshFilter>().mesh = ModResource.GetMesh("plain_mesh");
            go.AddComponent<MeshRenderer>().material.color = Color.green;

            var _mc = go.AddComponent<MeshCollider>();
            _mc.sharedMesh = ModResource.GetMesh("plain_mesh");
            BlockBehaviour.myBounds.childColliders.Add(_mc);
            _mc.material.dynamicFriction = 1f;
            _mc.material.staticFriction = 1f;
            _mc.material.frictionCombine = PhysicMaterialCombine.Maximum;
            //Physics.IgnoreCollision(GetComponentInChildren<BoxCollider>(), _mc);

            go.AddComponent<Rigidbody>().isKinematic = true;
            go.transform.SetParent(transform);
            go.transform.rotation = transform.rotation;
            go.transform.localPosition = localPosition;

            return go;
        }
        GameObject createCylinder(string name, Vector3 localPosition)
        {
            var go = new GameObject(name);
            go.AddComponent<MeshFilter>().mesh = ModResource.GetMesh("cylinder_mesh");
            go.AddComponent<MeshRenderer>().material.color = Color.blue;

            var _mc = go.AddComponent<MeshCollider>();
            _mc.sharedMesh = ModResource.GetMesh("cylinder_mesh");
            BlockBehaviour.myBounds.childColliders.Add(_mc);
            _mc.material.dynamicFriction = 1f;
            _mc.material.staticFriction = 1f;
            _mc.material.frictionCombine = PhysicMaterialCombine.Maximum;
            //Physics.IgnoreCollision(_mc, GetComponentInChildren<BoxCollider>());

            go.AddComponent<Rigidbody>().isKinematic = true;
            go.transform.SetParent(transform);
            go.transform.rotation = transform.rotation;
            go.transform.localPosition = localPosition;

            return go;
        }

    }


    public override void SimulateFixedUpdateAlways()
    {
        var rigid = plain1.GetComponent<Rigidbody>();
        Vector3 pos = rigid.position;
        Vector3 temp = -transform.right * (BlockBehaviour.Flipped ? 1f : -1f) * speedSlider.Value * Time.fixedDeltaTime;
        rigid.position += temp;
        rigid.MovePosition(pos);

        var rigid1 = cylinder1.GetComponent<Rigidbody>();
        Quaternion rot = rigid1.rotation;
        Quaternion angle = Quaternion.AngleAxis(speedSlider.Value * Time.fixedDeltaTime, cylinder1.transform.forward * (BlockBehaviour.Flipped ? 1f : -1f));
        rigid1.rotation *= angle;
        rigid1.MoveRotation(rot);

        var rigid2 = cylinder2.GetComponent<Rigidbody>();
        Quaternion rot1 = rigid2.rotation;
        Quaternion angle1 = Quaternion.AngleAxis(speedSlider.Value * Time.fixedDeltaTime, cylinder2.transform.forward * (BlockBehaviour.Flipped ? 1f : -1f));
        rigid2.rotation *= angle1;
        rigid2.MoveRotation(rot1);
    }


}

