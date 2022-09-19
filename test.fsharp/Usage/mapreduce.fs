#light

namespace test.fsharp
 
 module MapReduceExample = 
    // Simple example of map-reduce  in F#
    // Counts the total numbers of each animal
     
    // Map function for our problem domain
    let mapfunc (k,v) =
        v |> Seq.map (fun(pet) -> (pet, 1))
     
    // Reduce function for our problem domain
    let reducefunc (k,(vs:seq<int>)) =
        let count = vs |> Seq.sum
        k, Seq.ofList([count])
     
    // Performs map-reduce operation on a given set of input tuples
    let mapreduce map reduce (inputs:seq<_*_>) =
        let intermediates = inputs |> Seq.map map |> Seq.concat
        let groupings = intermediates |> Seq.groupBy fst |> Seq.map (fun(x,y) -> x, Seq.map snd y)
        let results = groupings |> Seq.map reduce
        results
 
