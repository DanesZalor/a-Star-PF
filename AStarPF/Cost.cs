namespace AStarPF;
using System;

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