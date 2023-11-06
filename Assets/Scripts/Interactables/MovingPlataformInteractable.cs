using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataformInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject plataformPanel;
    private bool plataformMove;

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
        Debug.Log("Interacted with Moving Plataform");
        plataformMove = !plataformMove;
        plataformPanel.GetComponent<Animator>().SetBool("IsActive", plataformMove);
    }
}
