// Loosely follows the "Thinking functionally" series
// https://fsharpforfunandprofit.com/posts/thinking-functionally-intro/


#if !FAKE
    #r "netstandard" // windows
    #r "Facades/netstandard" // mono
#endif


// TEST: Currying
// normal version of multiply
let result  = 3 * 5

// multiply as a one parameter function
let intermediateFn1 = (*) 3   // return multiply with "3" baked in
let r1  = intermediateFn1 5

// normal version of printfn
let r2  = printfn "x=%i y=%i" 3 5

// printfn as a one parameter function
let intermediateFn2 = printfn "x=%i y=%i" 3  // "3" is baked in
let r3  = intermediateFn2 5


// TEST: Lists, Seqs, Sets
let l0 = List.empty
let l1 = [1;2]
let s = Set.empty.Add(3).Add(5)

let listOfTuples = [(1,"a"); (2,"b"); (3,"b"); (4,"a")]
listOfTuples |> List.find ( fun (x,y) -> y = "b")


