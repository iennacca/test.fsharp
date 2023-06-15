namespace test.fsharp
 
module Utilities =
    type TraceBuilder() =
        member this.Bind(m, f) =
            match m with
            | None ->
                printfn "Binding with None. Exiting."
            | Some a ->
                printfn "Binding with Some(%A). Continuing" a
            Option.bind f m

        member this.Return(x) =
            printfn "Returning an unwrapped %A as an option" x
            Some x

        member this.ReturnFrom(m) =
            printfn "Returning an option (%A) directly" m
            m

        member this.Zero() =
            printfn "Zero"
            None

