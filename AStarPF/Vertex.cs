using System.Numerics;
using System.Collections.Generic;

namespace AStarPF;

public class Vertex{

    private uint _index; public uint Index { get => _index; } 
    private Vector3 _location; public Vector3 Location { get => _location;}

    private List<Vertex> _neighbors;
    public List<Vertex> neighbors { 
        get => _neighbors;
        set {
            if(_neighbors == null)  
                _neighbors = value;
            else
                throw new Exception("Vertex.neighbors is one time set only");
        }
    }

    public Vertex(uint idx, Vector3 loc){
        _index = idx;
        _location = loc;
        neighbors = new List<Vertex>();
    }

}

