using System.Numerics;
using System.Collections.Generic;

namespace AStarPF;

public class Vertex{
    
    public class Cost{
        private float _g, _h;
        public float G { get => _g; }
        public float H { get => _h; }
        public float F { get=> _g+_h;}

        public Cost(float g, float h){
            this._g = g;
            this._h = h;
        }
    }

    private uint _index; public uint Index { get => _index; } 
    private Vector3 _location; public Vector3 Location { get => _location;}
    public List<Vertex> neighbors;
    
    // changeable states
    public Cost? cost;
    public bool visited = false;
    public bool consumed = false;
    public Vertex? prevVertex = null;

    public Vertex(uint idx, Vector3 loc){
        _index = idx;
        _location = loc;
        neighbors = new List<Vertex>();
    }

}

