namespace test.fsharp

open System.Text.RegularExpressions

module WordCount = 
    let tokenizeText textInput = 
        let text:string = textInput
        text.Split  [|' '; '.'|]
    let printValue v = printfn "%A" v

    let mapValues values = values |> Seq.map (fun m -> (m,1))

    let reduceValues values =
        values |> Seq.fold 
            (fun m v -> match Map.tryFind v m with
                        | None -> Map.add v 1 m
                        | Some c -> Map.add v (c+1) m)
            Map.empty
