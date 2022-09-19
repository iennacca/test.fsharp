// Thinking functionally series (bind)
// https://fsharpforfunandprofit.com/posts/computation-expressions-continuations/
open System


let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top/bottom)

/// TOPIC: What we want to avoid (the tower of doom)
let divideByWorkflowDoom x y w z =
    let a = x |> divideBy y
    match a with
    | None -> None  // give up
    | Some a' ->    // keep going
        let b = a' |> divideBy w
        match b with
        | None -> None  // give up
        | Some b' ->    // keep going
            let c = b' |> divideBy z
            match c with
            | None -> None  // give up
            | Some c' ->    // keep going
                //return
                Some c'


/// TOPIC: sample builder type that uses Option.bind
/// what computation expressions can do
type MaybeBuilder() =
    member this.Bind(m,f) = Option.bind f m
    member this.Return(x) = Some x

let maybe = new MaybeBuilder()

let divideByWorkflowCompExpr x y w z = 
    maybe {
        let! a = x |> divideBy y
        let! b = a |> divideBy w
        let! c = b |> divideBy z
        return c
    }

/// TOPIC: TraceBuilder
/// Demonstrating usage of do!, Zero()
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

// make an instance of the workflow
let trace = new TraceBuilder()

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
