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

    [Fact] public void TestingSP1(){

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

        //Console.WriteLine(Graph.ToString(g));
        //Console.WriteLine(g.printShortestPath( new Vector3(0,0,0), new Vector3(2,2,0) ));

        Assert.Equal(g.getShortestPathIndexes(new Vector3(0,0,0), new Vector3(2,2,0)), new uint[]{0,4,7,8});
    }

    [Fact] public void TestingSP2(){

        Graph g = new Graph(new (Vector3, uint[])[]{
            // 0-5
            (new Vector3(0,0,0), new uint[]{1,6,7}), 
            (new Vector3(1,0,0), new uint[]{0,2,6,7,8}), 
            (new Vector3(2,0,0), new uint[]{1,3,7,8,9}), 
            (new Vector3(3,0,0), new uint[]{2,8,9,10,4}), 
            (new Vector3(4,0,0), new uint[]{3,5,9,10,11}), 
            (new Vector3(5,0,0), new uint[]{4,10,11}), 
            // 6-11
            (new Vector3(0,1,0), new uint[]{0,1,7,12}), 
            (new Vector3(1,1,0), new uint[]{0,1,2,6,8,12,13}), 
            (new Vector3(2,1,0), new uint[]{1,2,3,7,9,13,14}), 
            (new Vector3(3,1,0), new uint[]{2,3,4,8,10,13,14}), 
            (new Vector3(4,1,0), new uint[]{3,4,5,9,11,14,15}), 
            (new Vector3(5,1,0), new uint[]{4,5,10,15}), 
            // 12-15
            (new Vector3(0,2,0), new uint[]{6,7,16}), 
            (new Vector3(2,2,0), new uint[]{7,8,9,14}), 
            (new Vector3(3,2,0), new uint[]{13,8,9,10}), 
            (new Vector3(5,2,0), new uint[]{10,11,17}),
            // 16-17
            (new Vector3(0,3,0), new uint[]{12,18,19}), 
            (new Vector3(5,3,0), new uint[]{15,22,23}), 
            //18-23
            (new Vector3(0,4,0), new uint[]{16,19,24,25}), 
            (new Vector3(1,4,0), new uint[]{16,18,20,24,25,26}), 
            (new Vector3(2,4,0), new uint[]{19,21,25,26,27}), 
            (new Vector3(3,4,0), new uint[]{20,22,26,27,28}), 
            (new Vector3(4,4,0), new uint[]{17,21,23,27,28,29}), 
            (new Vector3(5,4,0), new uint[]{17,22,28,29}), 
            //24-29
            (new Vector3(0,5,0), new uint[]{18,19,25}), 
            (new Vector3(1,5,0), new uint[]{24,18,19,20,26}), 
            (new Vector3(2,5,0), new uint[]{25,19,20,21,27}), 
            (new Vector3(3,5,0), new uint[]{20,21,22,26,28}), 
            (new Vector3(4,5,0), new uint[]{21,22,23,27,29}), 
            (new Vector3(5,5,0), new uint[]{28,22,23}),
        });

        Console.WriteLine(Graph.ToString(g));
        
        // From 3 to 26 (can actually have 2 shortest paths)
        Assert.Equal(g.getShortestPathIndexes( new Vector3(3,0,0), new Vector3(2,5,0) ), new uint[]{3,8,7,12,16,19,26});
        
        // From 14 to 27
        Assert.Equal(g.getShortestPathIndexes(new Vector3(3,2,0), new Vector3(3,5,0)), new uint[]{14,10,15,17,22,27});
        
        // From 14 to 17
        Assert.Equal(g.getShortestPathIndexes(new Vector3(2,2,0), new Vector3(2,5,0)), new uint[]{13,7,12,16,19,26});

        Console.WriteLine(g.printShortestPath(new Vector3(3,2,0), new Vector3(3,5,0)));
    }

}