namespace AStarPF;

using System;
using System.Numerics;
using System.Collections.Generic;

public class Graph{

    public static string ToString(Graph g){
        string output = "Graph:\n";

        foreach(Vertex v in g.vertices){
            string nbrsStr = "";

            foreach(Vertex nbr in v.neighbors)
                nbrsStr += String.Format("{0}, ",nbr.Index);

            output += String.Format(" V:{0} [{1}]\n",v.Index, nbrsStr);
        }

        return output;
    }
    private Vertex[] vertices;

    public Graph(Vector3[] vecs, uint[,] edges){
        
        void Error(string msg){
            throw new Exception(
                String.Format("Error @ Graph.Graph(Vertex[], uint[,]): {0}",msg)
            );
        }

        /* Evaluate arguements*/{
            int edges_limit = (vecs.Length*(vecs.Length-1))/2;

            if(edges.GetLength(1) != 2)
                Error("arg1 should be a uint[n,2]");
            
            else if( edges.GetLength(0) > edges_limit )
                // theoretically, the maximum length[1] of edges should be <= n(n-1)/2
                Error(
                    String.Format(
                        "arg1: more edges than the complete graph of '{0}' vertices",vecs.Length
                    )
                );
            
            else if(edges.GetLength(0) == edges_limit )
                Error("appears to be a complete graph. Path finding is obsolete");
            
        }

        // set up the vertices
        vertices = new Vertex[vecs.Length];
        for(uint i = 0; i<vecs.Length; i++)
            vertices[i] = new Vertex(i, vecs[i]);

        for(uint i = 0; i<edges.GetLength(0); i++){
            uint v1 = edges[i,0];
            uint v2 = edges[i,1];

            if( Math.Max(v1,v2)>vecs.Length)
                Error(String.Format("vertex index '{0}' doesn't exist", Math.Max(v1,v2)));
            
            else if(
                vertices[v1].neighbors.Contains(vertices[v2]) ||
                vertices[v2].neighbors.Contains(vertices[v1]) 
            ) Error("some edges are duplicated");
            
            vertices[v1].neighbors.Add(vertices[v2]);
            vertices[v2].neighbors.Add(vertices[v1]);
        }
    }
    
}

