// Thinking functionally series (bind)
// https://fsharpforfunandprofit.com/posts/computation-expressions-bind/


// TEST: Different bind approaches
open System

let strToInt (s:string) = 
    try
        s |> int |> Some
    with :? FormatException ->
        None

// computation expression
type MaybeBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) = Some x

let maybe = new MaybeBuilder()

let stringAddWorkflow x y z = 
    maybe {
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
    }

let good1 = stringAddWorkflow "1" "23" "445"
let bad1 = stringAddWorkflow "1" "xyz" "445"

let (>>=) m f = Option.bind f m 

let strAdd str i = 
    maybe {
        let! a = strToInt str
        return a + i
    }

let good2 = strToInt "1" >>= strAdd "2" >>= strAdd "3"
let bad2 = strToInt "1" >>= strAdd "xyz" >>= strAdd "3"
