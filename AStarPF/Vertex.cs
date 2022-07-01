using System.Numerics;
using System.Collections.Generic;

namespace AStarPF;

public class Vertex{
    
    private uint _index; public uint Index { get => _index; } 
    private Vector3 _location; public Vector3 Location { get => _location;}
    public List<Vertex> neighbors;
    
    public Vertex(uint idx, Vector3 loc){
        _index = idx;
        _location = loc;
        neighbors = new List<Vertex>();
    }

}

