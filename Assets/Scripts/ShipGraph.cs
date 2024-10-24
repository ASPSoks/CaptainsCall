using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGraph : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();

    // Função para inicializar o gráfico do navio
    void Start()
    {
        // Exemplo: Definir os nós
        Node frenteA = new Node("Frente A", new Vector2(0, 5));
        Node meioA = new Node("Meio A", new Vector2(0, 0));
        
        Node canhaoEsq1 = new Node("Canhão Esq. 1", new Vector2(-5, 0));
        Node canhaoDir1 = new Node("Canhão Dir. 1", new Vector2(5, 0));
        
        Node canhaoEsq2 = new Node("Canhão Esq. 2", new Vector2(-5, -3));
        Node canhaoDir2 = new Node("Canhão Dir. 2", new Vector2(5, -3));
        
        Node salaControle = new Node("Sala de Controle", new Vector2(0, 3));
        Node deposito = new Node("Depósito", new Vector2(0, -3));

        // Adicionar arestas (conexões entre nós)
        frenteA.AddEdge(meioA, 1.0f); // Custo de mover de Frente A para Meio A
        meioA.AddEdge(canhaoEsq1, 2.0f); // Custo de mover de Meio A para Canhão Esq. 1
        meioA.AddEdge(canhaoDir1, 2.0f); // Custo de mover de Meio A para Canhão Dir. 1
        meioA.AddEdge(canhaoEsq2, 2.0f); // Custo de mover de Meio A para Canhão Esq. 2
        meioA.AddEdge(canhaoDir2, 2.0f); // Custo de mover de Meio A para Canhão Dir. 2
        frenteA.AddEdge(salaControle, 1.0f); // Custo de mover de Frente A para Sala de Controle
        meioA.AddEdge(deposito, 1.0f); // Custo de mover de Meio A para Depósito

        // Adicionar nós ao grafo
        nodes.Add(frenteA);
        nodes.Add(meioA);
        nodes.Add(canhaoEsq1);
        nodes.Add(canhaoDir1);
        nodes.Add(canhaoEsq2);
        nodes.Add(canhaoDir2);
        nodes.Add(salaControle);
        nodes.Add(deposito);
    }
}

public class ShipGraphNode : Node
{
    public List<ShipGraphNode> neighbors;

    public ShipGraphNode(string name, Vector2 pos) : base(name, pos)
    {
        neighbors = new List<ShipGraphNode>();
    }
}
