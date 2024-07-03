
let andThen f x =
    match x with
    | Some y -> y |> f
    | None -> None

let lift f x =
    match x with
    | Some y -> y |> f |> Some
    | None -> None
    
type OptionBuilder() =
    member _.Bind(x, f) = andThen f x
    member _.Return(x) = Some x
    member _.ReturnFrom(x) = x

let option = new OptionBuilder()


option {
    let! x' = Some 4
    let! y' = None
    return x' + y'
}

option {
    let! x' = Some 4
    let! y' = Some 100
    return x' + y'
}

