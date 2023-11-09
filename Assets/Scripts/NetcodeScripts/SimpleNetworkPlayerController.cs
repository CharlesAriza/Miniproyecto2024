using Unity.Netcode;
using UnityEngine;


public enum MethodToUse
{
    TwoPlayerInline = 1,
    TwoPlayersWithFunction = 2,
    UknownPlayersInline = 3,
    UnknownPlayersOptimal = 4,
}

public class SimpleNetworkPlayerController : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;  // Fuerza del salto

    [SerializeField] Material clientMaterial;
    [SerializeField] Material hostMaterial;

    [SerializeField] MethodToUse methodToUse=MethodToUse.TwoPlayerInline;


    private void Start()
    {
        MeshRenderer playerRenderer = GetComponent<MeshRenderer>();

        switch (methodToUse)
        {
            case MethodToUse.TwoPlayerInline:
                Debug.Log("Using inline code that only works with two players, if there are more it won't be consistent");
                if (IsServer)
                {
					 //Gestiono los objetos en el host
                    if (IsOwner)
                    {
                        playerRenderer.material = hostMaterial;
                    }
                    else
                    {
                        playerRenderer.material = clientMaterial;
                    }
                }
                else
                {
                    //Gestiono los objetos en los clientes
                    if (IsOwner)
                    {
                        playerRenderer.material = clientMaterial;
                    }
                    else 
                    {
                        playerRenderer.material = hostMaterial;
                    }                  
                }
                break;
            case MethodToUse.TwoPlayersWithFunction:
                if (IsClientObject_LimitedTwoPlayers(IsServer, IsOwner))
                {
                    playerRenderer.material = clientMaterial;
                }
                else
                {
                    playerRenderer.material = hostMaterial;
                }
                //Con operador Ternario sería:
                //playerRenderer.material = IsClientObject_LimitedTwoPlayers(IsServer, IsOwner) ? clientMaterial: hostMaterial;
                break;
            case MethodToUse.UknownPlayersInline:
                Debug.Log("Using inline code that  works with multiple players");
                if (IsServer)
                {
					//Gestiono los objetos en el host
                    if (IsOwner)
                    {
                        playerRenderer.material = hostMaterial;
                    }
                    else
                    {
                        playerRenderer.material = clientMaterial;
                    }
                }
                else
                {
                    //Gestiono los objetos en los clientes
                    if (IsOwner)
                    {
                        playerRenderer.material = clientMaterial;
                    }
                    else if (IsOwnedByServer)
                    {
                        //Es necesario esto, pues ya cuando hay más de 2 players, podría ser el player 3 o 4 p.e
                        playerRenderer.material = hostMaterial;
                    }
                    else
                    {
                        playerRenderer.material = clientMaterial;
                    }
                }
                break;
            case MethodToUse.UnknownPlayersOptimal:
                if(IsOwnedByServer)
                {
                    playerRenderer.material = hostMaterial;
                }
                else
                {
                    playerRenderer.material = clientMaterial;
                }
                //Con operador Ternario sería:
                //playerRenderer.material = IsOwnedByServer ? hostMaterial: clientMaterial;
                break;
            default:
                break;
        }
    }

    
	

    public static bool IsClientObject_LimitedTwoPlayers(bool isServer, bool isOwner)
    {
        return ((!isServer && isOwner) || (isServer && !isOwner));
    }

	
	void FixedUpdate()
    {
        if (IsOwner)
        {
            Vector3 newPosition = transform.localPosition;
            if (Input.GetKey(KeyCode.W))
            {
                newPosition.z += speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newPosition.z -= speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newPosition.x -= speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newPosition.x += speed * Time.deltaTime;
            }
            if (newPosition != transform.localPosition)
            {
                transform.localPosition = newPosition;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (newPosition != transform.localPosition)
            {
                transform.localPosition = newPosition;
            }
        }
    }
}
