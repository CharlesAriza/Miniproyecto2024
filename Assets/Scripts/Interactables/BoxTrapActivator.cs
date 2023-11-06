using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrapActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject CaptureBox;
    private bool TrapUp;

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        Debug.Log("Interacted with Capture Box");
        TrapUp = !TrapUp;
        CaptureBox.GetComponent<Animator>().SetBool("IsActive", TrapUp);
    }
}
