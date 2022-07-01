namespace AStarPF;

using System;
using System.Collections.Generic;

public class Graph{

    Vertex[] vertices;

    public Graph(Vertex[] verts, uint[,] edges){
        
        void Error(string msg){
            throw new Exception(
                String.Format("Error @ Graph.Graph(Vertex[], uint[,]):\n {0}",msg)
            );
        }

        /* Evaluate arguements*/{

            if(edges.GetLength(1) != 2)
                Error("arg1 should be a uint[n,2]");
            
            else if( (verts.Length*(verts.Length-1))/2 > edges.GetLength(0) )
                // theoretically, the maximum length[1] of edges should be <= n(n-1)/2
                Error("arg1: more edges than the complet graph of '"+Convert.ToString(verts.Length)+"' vertices");
        }

        vertices = verts;
        

        for(int i = 0; i<edges.Length; i++){
            uint v1 = edges[i,0];
            uint v2 = edges[i,1];

            if(
                vertices[v1].neighbors.Contains(vertices[v2]) ||
                vertices[v2].neighbors.Contains(vertices[v1]) 
            ) Error("some edges are duplicated");
            
            vertices[v1].neighbors.Add(vertices[v2]);
            vertices[v2].neighbors.Add(vertices[v1]);
        }
    }
    
}

