namespace test.fsharp

module Shapes = 
    type Shape =        // define a "union" of alternative structures
    | Circle of int 
    | Rectangle of int * int
    | Polygon of (int * int) list
    | Point of (int * int) 

    let draw shape =    // define a function "draw" with a shape param
      match shape with
      | Circle radius -> 
          printfn "The circle has a radius of %d" radius
      | Rectangle (height,width) -> 
          printfn "The rectangle is %d high by %d wide" height width
      | Polygon points -> 
          printfn "The polygon is made of these points %A" points
      | _ -> printfn "I don't recognize this shape"

