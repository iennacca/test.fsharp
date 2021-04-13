module CityLocationUnion

  type LatDirection = 
    | N = 0
    | S = 1

  type LongDirection = 
    | E = 2
    | W = 3

  type Coordinates = Coordinates of  latval:float * latdir:LatDirection * longval:float * longdir:LongDirection 
  type City = City of name:string * loc:Coordinates 

  let showCoordinates (c: Coordinates) = 
    let  (Coordinates (latval, latdir, longval, longdir)) = c
    printfn "%f%A, %f%A"  latval latdir longval longdir
