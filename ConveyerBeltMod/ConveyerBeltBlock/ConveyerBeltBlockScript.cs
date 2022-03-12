using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;

class ConveyerBeltBlockScript : BlockScript
{
    [SerializeField]
    private GameObject plain1, plain2;
    [SerializeField]
    private GameObject cylinder1, cylinder2;

    public override void SafeAwake()
    {
        base.SafeAwake();
    }

    public override void OnBlockPlaced()
    {
        plain1 = createPlain("Plain 1", new Vector3(0f, /*0.1875f*/0.23f, 0.75f));
        //plain2 = createPlain("Plain 2", new Vector3(0f, -0.1875f, 0.75f));
        //cylinder1 = createCylinder("Cylinder 1", new Vector3(1.25f, 0f, 0.75f));
        //cylinder2 = createCylinder("Cylinder 2", new Vector3(-1.25f, 0f, 0.75f));
        //GetComponent<Rigidbody>().centerOfMass = plain1.transform.localPosition;
        GameObject createPlain(string name,Vector3 localPosition)
        {
            var go = new GameObject(name);
            go.AddComponent<MeshFilter>().mesh = ModResource.GetMesh("plain_mesh");
            go.AddComponent<MeshRenderer>().material.color = Color.green;

            var _mc = go.AddComponent<MeshCollider>();
            _mc.sharedMesh = ModResource.GetMesh("plain_mesh");
            _mc.convex = true;
            BlockBehaviour.myBounds.childColliders.Add(_mc);
            Physics.IgnoreCollision(GetComponentInChildren<BoxCollider>(), _mc);

            go.AddComponent<Rigidbody>()/*.isKinematic = true*/;
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
            _mc.convex = true;
            BlockBehaviour.myBounds.childColliders.Add(_mc);
            Physics.IgnoreCollision(_mc, GetComponentInChildren<BoxCollider>());

            //go.AddComponent<Rigidbody>().isKinematic = true;
            go.transform.SetParent(transform);
            go.transform.rotation = transform.rotation;
            go.transform.localPosition = localPosition;

            return go;
        }
    }

    public override void OnSimulateStart()
    {
        Physics.IgnoreCollision(GetComponentInChildren<BoxCollider>(), plain1.GetComponent<MeshCollider>());
        plain1.GetComponent<Rigidbody>().isKinematic = true;
        //Physics.IgnoreCollision(GetComponentInChildren<BoxCollider>(), plain2.GetComponent<MeshCollider>());
        //Physics.IgnoreCollision(GetComponentInChildren<BoxCollider>(), cylinder1.GetComponent<MeshCollider>());
        //Physics.IgnoreCollision(GetComponentInChildren<BoxCollider>(), cylinder2.GetComponent<MeshCollider>());
    }
}

