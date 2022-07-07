using Xunit;

using AStarPF;
using System.Numerics;
using System.Collections.Generic;
using System;

namespace Tests;

public class Map2Test
{   
    private static Graph g;
    public Map2Test(){
        //Console.WriteLine("setup");
        if(g!=null) return;

        byte[,] map = new byte[17,19]{ // [height, width]
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, // 0-18
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, // 19-37
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, // 38-56
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, // 57-75
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1}, // 76-92
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1}, // 93-109
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1}, // 110-126
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1}, // 127-143
            {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1},
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1},
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1},
            {1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        List<(Vector3, uint)> vecs = new List<(Vector3,uint)>();
        uint vidx = 0;
        // setup the grid
        for(int y=0; y<17; y++)
            for(int x=0;x<19; x++)
                if(map[y,x] == 1){
                    vecs.Add(
                        (new Vector3(x,y,0),vidx++)
                    );                    
                }
        
        List<uint[]> neighbors = new List<uint[]>();

        
        foreach((Vector3,uint) v in vecs){
            
            List<uint> nbrs = new List<uint>();
            
            for(int y_offset = -1; y_offset<=1; y_offset++){
                for(int x_offset = -1; x_offset<=1; x_offset++){
                    
                    int tempX = (int)v.Item1.X + x_offset;
                    int tempY = (int)v.Item1.Y + y_offset;
                    
                    if( x_offset == 0 && y_offset == 0) continue;
                    else if(
                        tempX >= 0 && tempX < 19 &&
                        tempY >= 0 && tempY < 17 &&
                        map[tempY, tempX] == 1
                    ){
                        foreach((Vector3,uint) vcheck in vecs)
                            if(vcheck.Item1.X == tempX && vcheck.Item1.Y == tempY){
                                nbrs.Add(vcheck.Item2);
                                break;
                            }
                        
                    }
                }
            }       
            neighbors.Add(nbrs.ToArray());
        }

        Assert.Equal(neighbors.Count, vecs.Count);

        (Vector3, uint[])[] arg0 = new (Vector3,uint[])[vecs.Count];

        for(int i = 0; i<arg0.Length; i++)
            arg0[i] = (vecs[i].Item1, neighbors[i]);
        
        g = new Graph(arg0);
    }
    
    [Fact]
    public void sex(){
        //Console.WriteLine(Graph.ToString(g));
        Assert.Equal(1,1);
    }

    [Theory]
    [InlineData(0, 5, new uint[]{0,1,2,3,4,5})]
    [InlineData(79, 81, new uint[]{79,61,80,81})]
    [InlineData(130, 131, new uint[]{130,113,96,79,61,80,97,114,131})]
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