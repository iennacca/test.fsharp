open System

type Error = 
    | InvalidParameterError

// TOPIC: Testing ResultBuilder
type ResultBuilder () =
    member this.Errors = List.empty
    member this.Bind (m, f) =
        match m with
        | Error e ->
            printfn "Error: %A" e
        | Ok a ->
            printfn "Binding with Some(%A). Continuing" a
        Result.bind f m
    member this.Return (x) = 
        Ok x

let (>>=) m f = Result.bind f m 
let result = new ResultBuilder ()

type Accumulator = private { t1:int; t2:int; t3:int }

module Accumulator = 
    let transform i = 
        if i > 0 then Ok i 
        else Error InvalidParameterError

    let create i1 i2 i3 = 
        result {
            let! v1 = transform i1
            let! v2 = transform i2
            let! v3 = transform i3
            return { t1=v1; t2=v2; t3=v3 }
        }
    
    let make i = 
        i |> transform >>= transform >>= transform

let good = Accumulator.create 1 2 3
let bad = Accumulator.create 0 1 2
let umm = Accumulator.make 1 
