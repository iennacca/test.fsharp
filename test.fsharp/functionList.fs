namespace test.fsharp
open System

module FunctionList = 
    let traceMsg msg v= printfn "%s: %f" msg v; v
    let square (x:float) = x * x
    let sqrt x = Math.Sqrt x
    let traceOp op = [
        traceMsg "Input";
        op;
        traceMsg "Output";
    ]
    let traceSquare x =
        let fns = traceOp square 
        List.reduce(>>) fns x
