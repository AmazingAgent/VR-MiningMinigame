using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material activeMat;
    [SerializeField] private Material inactiveMat;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AudioSource soundButtonDown;
    [SerializeField] private AudioSource soundButtonUp;
    private bool isActive = true;
    private Vector3 defaultPos;
    private Vector3 targetPos;
    void Start()
    {
        defaultPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != targetPos)
        {
            MoveButton();
        }
    }

    private void MoveButton()
    {
        if (transform.localPosition.z > targetPos.z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - moveSpeed * Time.deltaTime);
            if (transform.localPosition.z < targetPos.z)
            {
                transform.localPosition = targetPos;
            }
        }
        if (transform.localPosition.z < targetPos.z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + moveSpeed * Time.deltaTime);
            if (transform.localPosition.z > targetPos.z)
            {
                transform.localPosition = targetPos;
            }
        }
    }

    public void ActivateButton()
    {
        if (!isActive) soundButtonUp.Play();

        isActive = true;
        meshRenderer.material = activeMat;
        targetPos = defaultPos;
        
    }

    public void DeactivateButton()
    {
        if (isActive) soundButtonDown.Play();

        isActive = false;
        meshRenderer.material = inactiveMat;
        targetPos = defaultPos + new Vector3(0, 0, -0.3f);
    }
}
