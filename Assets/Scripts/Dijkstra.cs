using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    // Algoritmo de Dijkstra para encontrar o caminho mais curto
    public static List<Node> FindShortestPath(Node startNode, Node endNode)
    {
        // Criar uma fila de prioridade para os nós não visitados
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>(); // Corrigido erro de digitação aqui
        List<Node> unvisited = new List<Node>();

        // Inicializar distâncias e lista de não visitados
        foreach (Node node in GameObject.FindObjectOfType<ShipGraph>().nodes)
        {
            distances[node] = float.MaxValue;
            previous[node] = null;
            unvisited.Add(node);
        }
        distances[startNode] = 0;

        while (unvisited.Count > 0)
        {
            // Obter o nó com a menor distância
            Node currentNode = null;
            float shortestDistance = float.MaxValue;

            foreach (Node node in unvisited)
            {
                if (distances[node] < shortestDistance)
                {
                    shortestDistance = distances[node];
                    currentNode = node;
                }
            }

            // Remover o nó atual da lista de não visitados
            unvisited.Remove(currentNode);

            // Se chegamos ao destino, reconstruir o caminho
            if (currentNode == endNode)
            {
                List<Node> path = new List<Node>();
                while (previous[currentNode] != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = previous[currentNode];
                }
                path.Insert(0, startNode); // Incluir o nó inicial
                return path; // Retornar o caminho mais curto
            }

            // Calcular distâncias para os vizinhos
            foreach (Edge edge in currentNode.edges)
            {
                float alt = distances[currentNode] + edge.weight;
                if (alt < distances[edge.endNode])
                {
                    distances[edge.endNode] = alt;
                    previous[edge.endNode] = currentNode;
                }
            }   
        }

        return null; // Nenhum caminho encontrado
    }
}
    