using System.Collections.Generic;
using UnityEngine;

public class BuoyancyObject : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] List<Transform> floaters;

    [Header("Water Drag Settings")]
    [SerializeField] float underWaterDrag;
    [SerializeField] float underWaterAngularDrag;

    [Header("Air Drag settings")]
    [SerializeField] float airDrag;
    [SerializeField] float airAngularDrag;

    [Header("Floating Settings")]
    [SerializeField] float floatingPower;
    [SerializeField] float waterHeight;
    [SerializeField] bool addFloatersAutomatically;

    bool isUnderwater;
    int floatersUnderWater;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!addFloatersAutomatically )
        {
            if (floaters.Count > 1) return;
            floaters.Add(transform);
            return;
        }
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                floaters.Add(child);
            }
        }
        else
        {
            floaters.Add(transform);
        }
    }

    private void FixedUpdate()
    {
        // Initialize counter for floaters under water
        floatersUnderWater = 0;

        for (int i = 0; i < floaters.Count; i++)
        {
            waterHeight = OceanManager.Instance.GetWaterHeightAtPosition(floaters[i].position);

            float difference = floaters[i].position.y - waterHeight;


            if (difference < 0)
            {
                Vector3 buoyancyForce = Vector3.up * floatingPower * Mathf.Abs(difference);

                rb.AddForceAtPosition(buoyancyForce, floaters[i].position, ForceMode.Force);

                floatersUnderWater++;
                if (!isUnderwater)
                {
                    isUnderwater = true;
                    SwitchDragMode();
                }
            }
        }

        if (isUnderwater && floatersUnderWater == 0)
        {
            isUnderwater = false;
            SwitchDragMode();
        }
    }

    void SwitchDragMode()
    {
        float currentLinearWaterDrag = isUnderwater ? underWaterDrag : airDrag;
        float currentAngularWaterDrag = isUnderwater ? underWaterAngularDrag : airAngularDrag;

        rb.linearDamping = currentLinearWaterDrag;
        rb.angularDamping = currentAngularWaterDrag;
    }
}
