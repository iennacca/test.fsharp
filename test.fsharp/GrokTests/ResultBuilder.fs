namespace GrokTests

module Core =
    open System
    open System.Collections

    type ResultBuilder () =
        member this.Bind (m, f) =
            match m with
            | Error e ->
                printfn "Error: %A" e
            | Ok a ->
                printfn $"Binding with Ok({a}). Continuing" 
            Result.bind f m
        member this.Return(x) = 
            printfn $"Returns {x}"
            Ok x
        member this.ReturnFrom(x) =
            printfn $"Returning {x}"
            x
        member this.Zero (x) = 
            None
        member this.Combine (m, f) = 
            Result.bind f m
        member this.MergeSources (result1, result2) =
            match result1, result2 with
            | Ok (ok1), Ok (ok2) -> 
                printfn $"Merge results: {ok1} {ok2}"
                Ok (ok1, ok2) 
            | Error errs1, Ok _ -> Error errs1
            | Ok _, Error errs2 -> Error errs2
            | Error errs1, Error errs2 -> 
                printfn $"Merge errors: {errs1} :: {errs2}"
                Error (errs1 @ errs2)  

    let (>>=) m f = Result.bind f m 
    let result = new ResultBuilder ()
