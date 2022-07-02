using Xunit;

using AStarPF;
using System.Numerics;
using System.Collections.Generic;
using System;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        (Vector3, uint[]) pair = (new Vector3(0,0,1), new uint[]{0,2,4});
        Console.WriteLine(pair.Item1.Z);
        Console.WriteLine(pair.Item2[2]);
    }



    [Fact]
    public void Test2(){
        
        Graph g = new Graph(
            new Vector3[]{
                new Vector3(0,0,0),
                new Vector3(1,0,0),
                new Vector3(0,1,0),
            },
            new uint[,]{
                {0,1},
                {0,2},
            }
        );

        //Console.WriteLine(Graph.ToString(g));

        Assert.Equal(1,1);
    }

}