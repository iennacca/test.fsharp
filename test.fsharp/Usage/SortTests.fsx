#if !FAKE
    #r "netstandard" // windows
    #r "Facades/netstandard" // mono
#endif

#load "../util/Utilities.fs"
#load "../Usage/quicksort.fs"
open test.fsharp

// make an instance of the workflow
let trace = new Utilities.TraceBuilder()

printfn "%A" (Sorter.quicksort [1;5;23;18;9;1;3]);;
printfn "%A" (Sorter.quicksort2 ["test";"hello";"maniac";"vermin"])

trace {
    do! Some (printfn "...expression that returns unit")
    do! Some (printfn "...another expression that returns unit")
    let! x = Some (1)
    return x
 } |> printfn "Result from do: %A"

// uses Zero()
trace {
    printfn "hello"
} |> printfn "Result for simple expression: %A" 
