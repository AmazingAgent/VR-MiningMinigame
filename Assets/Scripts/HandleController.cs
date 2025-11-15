using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    [SerializeField] private Transform handle;
    private Vector3 transformOffset;
    void Start()
    {
        transformOffset = transform.position - handle.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handle.position + transformOffset;
    }
}
