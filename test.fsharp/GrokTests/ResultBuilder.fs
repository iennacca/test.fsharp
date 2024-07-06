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
        printfn $"REturns {x}"
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

// Testing ResultBuilder
let validateName name = 
    match name with
    | null -> Error ["Name must not be null."]
    | "" -> Error ["Name must not be empty."]
    | _ -> Ok name

result {
    let! s1 = validateName "Jerry"
    and! s2 = validateName "Wendell"
    and! s3 = validateName "Chaves"
    return  (s1, s2, s3)
}

result {
    let! s1 = validateName ""
    and! s2 = validateName ""
    return (s1, s2)
}
