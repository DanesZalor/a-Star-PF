namespace AStarPF;

using System;
using System.Numerics;
using System.Collections.Generic;

public class Graph{
    
    /// <summary> for Debug/Developement only </summary>
    public static string ToString(Graph g){
        string output = "Graph:\n";

        foreach(Vertex v in g.vertices){
            string nbrsStr = "";

            foreach(Vertex nbr in v.neighbors)
                nbrsStr += String.Format("{0}, ",nbr.Index);


            // remove the last comma if any
            if(nbrsStr.Length>0) nbrsStr = nbrsStr.Substring(0,nbrsStr.Length - 2);

            output += String.Format("{0} [{1}]\n",v.Index, nbrsStr);
        }

        return output;
    }

    private Vertex[] vertices;

    public Graph((Vector3, uint[])[] nodes){
        void Error(string msg){
            throw new Exception(
                String.Format("!!@Graph.constructor(): {0}",msg)
            );
        }

        
        { // VALIDATE 
            if(nodes == null || nodes.Length==0)
                Error("arg0 cannot be empty");

            foreach( (Vector3, uint[]) node in nodes){
                foreach( (Vector3, uint[]) node2 in nodes){
                    
                    // if practically the same point
                    if( 
                        node != node2 &&
                        Vector3.Subtract(node.Item1, node2.Item1).Length() <= 0f
                    ) Error("2 or more vectors have equal points");
                }
            }
        }

        vertices = new Vertex[nodes.Length];

        // initialize vertices
        for(uint i = 0; i<nodes.Length; i++)
            vertices[i] = new Vertex(i, nodes[i].Item1);
        
        // set vertices neighbors
        for(uint i = 0; i<nodes.Length; i++){
            foreach(uint ndx in nodes[i].Item2)
                if(ndx!= i && !vertices[i].neighbors.Contains(vertices[ndx])){
                    vertices[i].neighbors.Add(vertices[ndx]);
                    vertices[ndx].neighbors.Add(vertices[i]);
                }
        }
    }

    private Vertex getClosestVertex(Vector3 from){
        
        int closestIdx = 0;

        for(int i = 1; i<vertices.Length; i++)
            if( 
                Vector3.Subtract(from, vertices[i].Location).LengthSquared() <
                Vector3.Subtract(from, vertices[closestIdx].Location).LengthSquared()
            ) closestIdx = i;
        
        return vertices[closestIdx];
    }

    public Vector3 getVertexLocation(int idx){
        return vertices[idx].Location;
    }

    /// heat map based path finding
    public List<Vertex> getShortestPath(Vector3 from, Vector3 to){
        
        // Initialization
        
            // H and G costs for vertices
            Cost[] costs = new Cost[vertices.Length];
            bool[] visited = new bool[vertices.Length];
            // index of the prevVertex of each vertex. -1 means not set
            int[] prevVertex = new int[vertices.Length]; Array.Fill(prevVertex, -1);
        

        Vertex start = getClosestVertex(from);
        Vertex end = getClosestVertex(to);

        Vertex curr = start;

        costs[curr.Index] = new Cost(0, Vector3.Subtract(curr.Location, end.Location).LengthSquared());


        for(int numOfVisited=0; numOfVisited <= vertices.Length || !visited[end.Index]; numOfVisited++){
            // assign cost to curr.neighbors
            foreach(Vertex cnbr in curr.neighbors){
                
                Cost tempcost = new Cost(
                    //(curr.prevVertex != null ? costs[curr.prevVertex.Index].G : 0)
                    (prevVertex[curr.Index] != -1 ? costs[prevVertex[curr.Index]].G : 0)
                    + Vector3.Subtract(curr.Location, cnbr.Location).LengthSquared(),  
                    Vector3.Subtract(curr.Location, end.Location).LengthSquared()
                );

                if(costs[cnbr.Index] == null || tempcost.G < costs[cnbr.Index].G ) 
                    costs[cnbr.Index] = tempcost;

                if(
                    //cnbr.prevVertex == null ||
                    prevVertex[cnbr.Index] == -1 ||
                    //costs[curr.Index].G < costs[cnbr.prevVertex.Index].G 
                    costs[curr.Index].G < costs[prevVertex[cnbr.Index]].G 
                )
                    //cnbr.prevVertex = curr;
                    prevVertex[cnbr.Index] = (int)curr.Index;
            }

            visited[curr.Index] = true;
            List<Vertex> unvisited_costed_vertices = new List<Vertex>();
            foreach(Vertex v in vertices)
                if(!visited[v.Index] && costs[v.Index] != null) unvisited_costed_vertices.Add(v);
            
            if(unvisited_costed_vertices.Count <= 0) break;

            Vertex leastF = unvisited_costed_vertices[0];
            foreach(Vertex ucv in unvisited_costed_vertices)
                if(costs[ucv.Index].F < costs[leastF.Index].F) leastF = ucv;

            curr = leastF;
        }

        List<Vertex> shortestPath = new List<Vertex>(new Vertex[]{end});

        while(shortestPath[0] != start && shortestPath.Count <= vertices.Length)
            //shortestPath = shortestPath.Prepend(shortestPath[0].prevVertex).ToList();
            shortestPath = shortestPath.Prepend(
                vertices[
                    prevVertex[shortestPath[0].Index]
                ]
            ).ToList();
                
        return shortestPath;
    }

    public uint[] getShortestPathIndexes(Vector3 from, Vector3 to){
        List<Vertex> sp = getShortestPath(from, to);

        uint[] pathIdx = new uint[sp.Count];

        for(int i = 0; i<pathIdx.Length; i++) pathIdx[i] = sp[i].Index;
        
        return pathIdx;
    }
}

