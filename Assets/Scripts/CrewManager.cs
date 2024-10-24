using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    public List<CrewMember> crewMembers; // Lista de todos os membros da tripulação no navio
    public ShipGraph shipGraph; // Referência ao sistema de grafo do navio

    public ShipGraphNode crewPosition; // Posição atual da tripulação (nó inicial)
    public ShipGraphNode targetCannon; // Nó do canhão alvo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Aciona o movimento da tripulação (exemplo: tecla de espaço)
        {
            // Encontra o caminho mais curto da posição da tripulação até o canhão alvo
            List<Node> path = Dijkstra.FindShortestPath(crewPosition, targetCannon); // Garante o tipo correto

            if (path != null)
            {
                Debug.Log("Caminho mais curto encontrado!");
                foreach (Node node in path)
                {
                    Debug.Log("Mover para: " + node.nodeName); // O nó possui nodeName
                }
            }
            else
            {
                Debug.Log("Nenhum caminho encontrado!");
            }
        }
    }

    public void AssignCrewToTask(ShipGraphNode targetNode)
    {
        CrewMember closestCrew = null;
        float shortestDistance = Mathf.Infinity;

        foreach (CrewMember crew in crewMembers)
        {
            if (!crew.IsBusy)
            {
                // Encontra o caminho mais curto e mede seu comprimento
                List<Node> path = Dijkstra.FindShortestPath(crew.currentNode, targetNode); // Usa o tipo Node
                if (path != null && path.Count < shortestDistance)
                {
                    shortestDistance = path.Count;
                    closestCrew = crew;
                }
            }
        }

        if (closestCrew != null)
        {
            // Atribui a tarefa e move o membro da tripulação
            closestCrew.AssignTask(targetNode);
        }
    }
}

public class CrewMember : MonoBehaviour
{
    public ShipGraphNode currentNode; // Posição atual do membro da tripulação
    public bool IsBusy = false;

    public ParticleSystem movementParticles; // Efeito de partículas para movimento
    public ParticleSystem repairParticles; // Efeito de partículas para reparo
    public LineRenderer lineRenderer; // Atribua isso no inspetor do Unity

    public void AssignTask(ShipGraphNode targetNode)
    {
        IsBusy = true;
        movementParticles.Play(); // Inicia o efeito de partículas de movimento
        StartCoroutine(MoveAlongPath(targetNode));
    }

    private IEnumerator MoveAlongPath(ShipGraphNode targetNode)
    {
        List<Node> path = Dijkstra.FindShortestPath(currentNode, targetNode); // Garante o tipo correto
        
        movementParticles.Play(); // Inicia o efeito de partículas durante o movimento
        DrawPath(path); // Visualiza o caminho
        
        foreach (Node node in path)
        {
            while (Vector3.Distance(transform.position, node.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, node.position, Time.deltaTime * 2);
                yield return null;
            }
            currentNode = (ShipGraphNode)node; // Faz cast do nó para ShipGraphNode se necessário
        }

        movementParticles.Stop(); // Para o efeito de partículas após a conclusão do movimento
        lineRenderer.positionCount = 0; // Limpa a linha após o movimento
        IsBusy = false;
    }

    private void DrawPath(List<Node> path)
    {
        lineRenderer.positionCount = path.Count;
        
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i].position);
        }
    }

    public void AssignRepairTask()
    {
        IsBusy = true;
        movementParticles.Play(); // Inicia o efeito de partículas de movimento
        StartCoroutine(RepairShip());
    }

    private IEnumerator RepairShip()
    {
        repairParticles.Play(); // Inicia o efeito de partículas de reparo

        float repairDuration = 2f; // Define a duração para o processo de reparo
        float repairStartTime = Time.time; // Registra o tempo de início

        while (Time.time < repairStartTime + repairDuration) // Verifica se a duração do reparo foi alcançada
        {
            // Implemente a lógica de reparo aqui, por exemplo, incremente a saúde do navio
            // Exemplo: shipHealth += repairAmount;

            yield return null; // espera até o próximo quadro
        }

        repairParticles.Stop(); // Para o efeito de partículas após a conclusão do reparo
        IsBusy = false; // Marca o membro da tripulação como não ocupado
    }
}
