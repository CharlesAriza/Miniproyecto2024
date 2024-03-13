using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    //Propiedad de la patruya.
    public void Initialise()
    {
        //Creamos un estado base.
        ChangeState(new PatrolState());
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Comprueba si el estado no es nulo.
        if (activeState != null)
        {
            //Si no es nulo que lo lleve a cabo. 
            activeState.Perform();
        }

    }
    public void ChangeState(BaseState newState)
    {
        //Comprueba que el estado no es nulo.
        if (activeState != null)
        {
            //Sale del estado actual.
            activeState.Exit();
        }
        //Cambia al nuevo estado.
        activeState = newState;
        if (activeState != null)
        {
            //Set el nuevo estado.
            activeState.stateMachine = this;
            //Asigna el estado al enemigo/agente.
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
