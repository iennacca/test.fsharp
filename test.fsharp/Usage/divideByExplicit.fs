namespace test.fsharp
open System

module DivideByExplicit = 

    let divideBy bottom top =
        if bottom = 0
        then None
        else Some(top/bottom)

    let divideByWorkflow x y w z = 
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
