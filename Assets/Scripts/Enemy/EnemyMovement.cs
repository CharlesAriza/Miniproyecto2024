using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Transform player;
    private bool canMove = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //Añadimos un offset para que vaya al pecho del jugador.
        Vector3 target = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        if (canMove && player != null)
        {
            // Mueve el objeto hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
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
