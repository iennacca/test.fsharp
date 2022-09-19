module CityLocationRecord

  type LatDirection = 
    | N = 0
    | S = 1

  type LongDirection = 
    | E = 2
    | W = 3

  type Coordinates = { latval:float; latdir:LatDirection; longval:float;  longdir:LongDirection }  
  type City = { name:string; loc:Coordinates } 

  let showCoordinates (c: Coordinates) = 
    printfn "%f%A, %f%A"  c.latval c.latdir c.longval c.longdir
