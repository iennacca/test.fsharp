namespace test.fsharp
open System

module DivideByBind = 

    let divideBy bottom top =
        if bottom = 0
        then None
        else Some(top/bottom)

    let pipeInto (m,f) =
       match m with
       | None -> 
           None
       | Some x -> 
           x |> f

    let bind (m,f) =
        Option.bind f m

    let return' x = Some x

    let divideByWorkflow x y w z = 
        pipeInto (x |> divideBy y, fun a ->
        pipeInto (a |> divideBy w, fun b ->
        pipeInto (b |> divideBy z, fun c ->
        return' c 
        )))

