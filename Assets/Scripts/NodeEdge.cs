using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe Node que representa um ponto do navio
public class Node
{
    public string nodeName;  // exemplo: "Frente A"
    public Vector2 position; // Posição no navio para visualização
    public List<Edge> edges; // Conexões para outros nós

    public Node(string name, Vector2 pos)
    {
        nodeName = name;
        position = pos;
        edges = new List<Edge>();
    }

    public void AddEdge(Node targetNode, float weight)
    {
        Edge newEdge = new Edge(this, targetNode, weight);
        edges.Add(newEdge);
    }
}

// Classe Edge que representa uma conexão entre dois nós
public class Edge
{
    public Node startNode;
    public Node endNode;
    public float weight; // Custo de movimento, tempo ou dificuldade

    public Edge(Node from, Node to, float cost)
    {
        startNode = from;
        endNode = to;
        weight = cost;
    }
}
