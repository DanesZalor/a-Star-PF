using Xunit;

using AStarPF;
using System.Numerics;
using System.Collections.Generic;
using System;

namespace Tests;

public class Map1Test
{   
    private static Graph g;
    public Map1Test(){
        //Console.WriteLine("setup");
        if(g!=null) return;

        g = new Graph(new (Vector3, uint[])[]{
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

        //Console.WriteLine(Graph.ToString(g));
    }

    [Theory] 
    //[InlineData( 3, 26,  new uint[]{3,8,7,12,16,19,26} )] // ambiguous result but still correct 
    [InlineData( 14, 27, new uint[]{14,10,15,17,22,27} )]
    [InlineData( 13, 26, new uint[]{13,7,12,16,19,26} )]
    [InlineData( 7, 23,  new uint[]{7,8,9,10,15,17,23} )]
    [InlineData( 12, 15,  new uint[]{12,7,8,9,10,15} )]
    
    public void TestingSP2(int from, int to, uint[] expected_res){

        uint[] actual_res = g.getShortestPathIndexes(
            g.getVertexLocation(from),
            g.getVertexLocation(to)
        );
        
        try{
            Assert.Equal(expected_res, actual_res);
        }catch(Exception e){
            throw new Exception(String.Format(
                "\n\tEXPECT: [{0}]\n\tACTUAL: [{1}]\n", 
                string.Join(",",expected_res), string.Join(",",actual_res) 
            ));
        }
    }

}