using Xunit;

using AStarPF;
using System.Numerics;
using System;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Vertex v = new Vertex(0,new Vector3(69,420,1337));
        Assert.Equal(v.Location.X,69);
        
    }

    [Fact]
    public void Test2(){
        
        Graph g = new Graph(
            new Vertex[]{
                new Vertex(0, new Vector3(0,0,0)),
                new Vertex(0, new Vector3(1,0,0))
            },
            new uint[,]{
                {0,1}
            }
        );

        Console.WriteLine(Graph.ToString(g));

        Assert.Equal(1,1);
    }

}