#load "TryParser.fs"

open TryParser

// tests
let parseMe = function
    | Date d   -> printfn "DateTime %A" d
    | Int 42   -> printfn "Bingo!"
    | Int i    -> printfn "Int32 %i" i
    | Single f -> printfn "Single %g" f
    | Double d -> printfn "Double %g" d // never hit, always parsed as float32
    | s        -> printfn "Don't know how to parse %A" s

parseMe "1/1/2002"
parseMe "2002"
parseMe "42"
