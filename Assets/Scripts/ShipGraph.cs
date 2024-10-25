using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGraph : MonoBehaviour
{
    public List<ShipGraphNode> nodes = new List<ShipGraphNode>(); // Use ShipGraphNode

    // Função para inicializar o gráfico do navio
    void Start()
    {
        // Exemplo: Definir os nós como ShipGraphNode
        ShipGraphNode frenteA = new ShipGraphNode("Frente A", new Vector2(0, 5));
        ShipGraphNode meioA = new ShipGraphNode("Meio A", new Vector2(0, 0));
        
        ShipGraphNode canhaoEsq1 = new ShipGraphNode("Canhão Esq. 1", new Vector2(-5, 0));
        ShipGraphNode canhaoDir1 = new ShipGraphNode("Canhão Dir. 1", new Vector2(5, 0));
        
        ShipGraphNode canhaoEsq2 = new ShipGraphNode("Canhão Esq. 2", new Vector2(-5, -3));
        ShipGraphNode canhaoDir2 = new ShipGraphNode("Canhão Dir. 2", new Vector2(5, -3));
        
        ShipGraphNode salaControle = new ShipGraphNode("Sala de Controle", new Vector2(0, 3));
        ShipGraphNode deposito = new ShipGraphNode("Depósito", new Vector2(0, -3));

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
