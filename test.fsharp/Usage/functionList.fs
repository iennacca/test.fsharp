namespace test.fsharp
open System

module FunctionList = 
    let traceMsg msg v = printfn "%s: %A" msg v; v
    let traceOps op = [
        traceMsg "Input";
        op;
        traceMsg "Output";
    ]
    let traceOp op =
        let fns = traceOps op
        List.reduce (>>) fns
