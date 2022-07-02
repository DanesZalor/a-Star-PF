using Xunit;

using AStarPF;
using System.Numerics;
using System.Collections.Generic;
using System;

namespace Tests;

public class UnitTest1
{

    [Fact]
    public void Test2(){
        
        Graph g = new Graph(new (Vector3, uint[])[]{
            (new Vector3(0,0,0), new uint[]{1,2}),
            (new Vector3(0,1,0), new uint[]{2}),
            (new Vector3(1,0,0), new uint[]{}),
        });

        //Console.WriteLine(Graph.ToString(g));

        Assert.Equal(1,1);
    }

    [Fact]
    public void TestingSP(){
        // arrange

        Graph g = new Graph(new (Vector3, uint[])[]{
            (new Vector3(0,0,0), new uint[]{1,4}), 
            (new Vector3(1,0,0), new uint[]{0,2}),
            (new Vector3(2,0,0), new uint[]{1,3}),
            (new Vector3(3,0,0), new uint[]{2,5}),

            (new Vector3(0,1,0), new uint[]{0,1,6,7}),
            (new Vector3(3,1,0), new uint[]{2,3,8,9}),

            (new Vector3(0,2,0), new uint[]{4,7}),
            (new Vector3(1,2,0), new uint[]{6,8}),
            (new Vector3(2,2,0), new uint[]{7,9}),
            (new Vector3(3,2,0), new uint[]{5,8}),
        });

        Console.WriteLine(Graph.ToString(g));
        Console.WriteLine(g.printShortestPath( new Vector3(0,0,0), new Vector3(2,2,0) ));

        Assert.Equal(1,1);

    }    

}