# A* Pathfinding

A vector based mapping path finding algorithm. The Graph will comprise of vertices each with their own Vector location to be used for the calculation of the Heuristics.

### PseudoCode
**Input**: `start:Vertex, end:Vertex`
```C#
Vertex curr = start
curr.cost = { G: 0, H: distance(curr.Location, end.Location) }

while (not all nodes are visited):
    foreach(cnbr as curr.neighbors):
        
        // G-cost = cost of the path
        // H-cost = heuristic location to end (i forgot why its curr instead of cnbr)
        tempCost = { 
            G: curr.prevVertex.G + distance(curr.Location - cnbr.Location)
            H: distance(curr.Location - end.Location)
        }

        if(cnbr.cost == null || tempCost.G < cnbr.cost.G )
            cnbr.cost = tempCost
        
        if(cnbr.prevVertex == null || curr.cost.G < cnbr.prevVertex.cost.G)
            cnbr.prevVertex = curr

    curr.visited = true

    unvisited_costed_vertices = // get all vertices in the graph
    // that are !visited && cost != null

    if(unvisited_costed_vertices is []) break

    // min( .cost.F ) among all unvisited_costed_vertices
    curr = leastFCost(unvisited_costed_vertices)

shortestPath = [end]

while(shortestPath[0] != start):
    shortestPath.Prepend( shortestPath[0].prevVertex )

return shortestPath
```

The main idea here is that, from the start, it expands and gives the neighbors costs and direction to where it came from to create an expanding heat map. 

 **`G-cost`**`= curr.prev.Gcost + length(neighbor.Location - curr.Location)`



