using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;

    class AccelerateZoneBlockScript:BlockScript
    {

    MSlider accelerateSpeedSlider;

    public override void SafeAwake()
    {
        base.SafeAwake();

        accelerateSpeedSlider = AddSlider("Accelerate", "accelerate", 1f, 0f, 5f);
    }
    public override void OnBlockPlaced()
    {
        transform.FindChild("Colliders").GetComponentInChildren<BoxCollider>().isTrigger = true;

        transform.FindChild("Vis").GetComponent<MeshRenderer>().enabled = false;


    }

    public override void OnSimulateTriggerStay(Collider collision)
    {
        base.OnSimulateTriggerStay(collision);

        if (collision.attachedRigidbody != null)
        {
            var rigid = collision.attachedRigidbody;
            rigid.AddForce(accelerateSpeedSlider.Value * rigid.velocity * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    public override void OnSimulateStart()
    {
        base.OnSimulateStart();
    
    }
}

