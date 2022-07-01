namespace AStarPF;

using System;
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

        for(int i = 0; i<verts.Length; i++){
            for(int j = 0; j<verts.Length; j++)
                if( i!=j && verts[i].Index == verts[j].Index)
                    Error("vertices have duplicate indexes");  
        }

        vertices = verts;        

        for(uint i = 0; i<edges.GetLength(0); i++){
            uint v1 = edges[i,0];
            uint v2 = edges[i,1];

            if( Math.Max(v1,v2)>verts.Length)
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

