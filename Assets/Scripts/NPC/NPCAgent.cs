using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCAgent : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator MoveTo(Vector3 target)
    {
        while (agent == null)
        {
            yield return null;
        }
        // Lance le déplacement
        agent.SetDestination(target);

        // Tant que le chemin n’est pas calculé ou que l’agent n’est pas encore arrivé :
        while (true)
        {
            // Si le calcul du chemin est terminé ET que la distance restante est inférieure ou égale à la distance d’arrêt
            if (agent.pathPending == false && agent.remainingDistance <= agent.stoppingDistance)
            {
                // On vérifie qu’il n’y a plus de chemin actif ou que la vitesse est nulle
                if (agent.hasPath == false || agent.velocity.sqrMagnitude == 0f)
                {
                    // Destination atteinte, on sort de la boucle
                    break;
                }
            }

            // On attend la fin de la frame avant de re-vérifier
            yield return null;
        }

        // Ici, la destination est atteinte. La coroutine se termine.
        yield return null;
    }

}
