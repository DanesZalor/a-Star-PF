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

    private (int, Vertex) getClosestVertex(Vector3 from){
        
        int closestIdx = 0;

        for(int i = 1; i<vertices.Length; i++)
            if( 
                Vector3.Subtract(from, vertices[i].Location).LengthSquared() <
                Vector3.Subtract(from, vertices[closestIdx].Location).LengthSquared()
            ) closestIdx = i;
        
        return (closestIdx, vertices[closestIdx]);
    }

    /// <return> List<Vertex> of the shortest path. Returns an empty list if there is no path </return>
    public List<Vertex> getShortestPath(Vector3 from, Vector3 to){
        
        // reset costs
        foreach(Vertex v in vertices){
            v.cost = null;
            v.visited = false;
            v.prevVertex = null;
        }

        Vertex start = getClosestVertex(from).Item2;
        Vertex end = getClosestVertex(to).Item2;

        start.cost = new Vertex.Cost(0, Vector3.Subtract(start.Location, end.Location).LengthSquared());
        //end.cost = new Vertex.Cost(start.cost.H, 0);

        Vertex curr = start;

        while(curr!=end){
            
            curr.visited = true;

            // set all curr.neighbor's cost
            foreach(Vertex nbrv in curr.neighbors){
                
                
                /*
                float gcost_abs_dist = Vector3.Subtract(start.Location, nbrv.Location).LengthSquared();
                float gcost_add_dist = 
                    Vector3.Subtract(start.Location, curr.Location).LengthSquared() + 
                    Vector3.Subtract(curr.Location, nbrv.Location).LengthSquared();
                */
                nbrv.cost = new Vertex.Cost(
                    Vector3.Subtract(start.Location, nbrv.Location).LengthSquared(),
                    Vector3.Subtract(end.Location, nbrv.Location).LengthSquared()
                );

                if(
                    nbrv.prevVertex==null 
                    || curr.cost.G < nbrv.prevVertex.cost.G
                )
                    nbrv.prevVertex = curr;

                // not sure if this is useful ngl
                /*
                else if( nbrv.prevVertex.prevVertex != null &&
                    nbrv.neighbors.Contains(nbrv.prevVertex.prevVertex)
                ) nbrv.prevVertex = nbrv.prevVertex.prevVertex;                
                */
            }

            // among the unvisited but costed vertices
            // set curr to be the one with the least F-cost
            List<Vertex> unvisited_costed_vertices = new List<Vertex>();
            foreach(Vertex v in vertices)
                if(!v.visited && v.cost != null)
                    unvisited_costed_vertices.Add(v);
            
            if(unvisited_costed_vertices.Count==0)
                return new List<Vertex>();

            Vertex leastF = unvisited_costed_vertices[0];
            for(int i=1; i<unvisited_costed_vertices.Count; i++){
                if( 
                    (unvisited_costed_vertices[i].cost.H == 0) || // if its the end
                    (unvisited_costed_vertices[i].cost.F < leastF.cost.F) || // get the lowest F cost
                    ( 
                        unvisited_costed_vertices[i].cost.F == leastF.cost.F && // 2nd prio is the H
                        unvisited_costed_vertices[i].cost.H < leastF.cost.H
                    )
                )
                    leastF = unvisited_costed_vertices[i];
            }

            curr = leastF;
        }

        curr.visited = true;
        // all visited nodes form a single path now

        List<Vertex> shortestPath = new List<Vertex>();
        shortestPath.Add(end);

        while(shortestPath[0] != start)
            shortestPath = shortestPath.Prepend(shortestPath[0].prevVertex).ToList();
        
        
        return shortestPath;
    }

    public uint[] getShortestPathIndexes(Vector3 from, Vector3 to){
        List<Vertex> sp = getShortestPath(from, to);
        uint[] pathIdx = new uint[sp.Count];

        for(int i = 0; i<pathIdx.Length; i++)
            pathIdx[i] = sp[i].Index;
        
        return pathIdx;
    }

    public string printShortestPath(Vector3 from, Vector3 to){
        List<Vertex> sp = getShortestPath(from, to);

        string output = String.Format("From {0} to {1}: [", getClosestVertex(from).Item1, getClosestVertex(to).Item1 );
        foreach(Vertex v in sp)
            output += String.Format("{0}-> ", v.Index); 
        
        output = output + "]";

        return output;
    }

}

