using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Transform player;
    private bool canMove = false;
    public NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //Añadimos un offset para que vaya al pecho del jugador.
        // new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        //Vector3 target = player.position + Vector3.up;
        if (canMove && player != null)
        {
            // Mueve el objeto hacia el jugador
            //transform.position =  Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            agent.destination = player.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
}
