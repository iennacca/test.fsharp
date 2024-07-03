#r "nuget:FSharpPlus"

open FSharpPlus

let r01' = map (fun x -> string (x + 10)) [ 1;2;3 ]
let r02' = map (fun x -> string (x + 10)) [|1;2;3|]
let r03' = map (fun x -> string (x + 10)) (Some 5)

let async5 = async.Return 5
let r05  = map (fun x -> string (x + 10)) async5
let r05' = Async.RunSynchronously r05

module TupleFst = let map f (a,b) = (f a, b)
module TupleSnd = let map f (a,b) = (a, f b)

let r07 = TupleFst.map (fun x -> string (x + 10)) (5, "something else")
let r08 = TupleSnd.map (fun x -> string (x + 10)) ("something else", 5)