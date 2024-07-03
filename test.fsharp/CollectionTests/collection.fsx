#if !FAKE
    #r "netstandard" // windows
    #r "Facades/netstandard" // mono
#endif

#load "setup.fs"
open collectiontest.setup

let houses = House.getRandom 20
let printHouse h =
    printf "Address: %A - Price: %A\n" h.Address h.Price 

// print out houses
houses |> Array.map printHouse
// get average price of all houses
houses |> Array.map (fun h -> h.Price |> double) |> Array.average
// get houses where price > 250000
houses |> Array.filter (fun h -> h.Price > 250_000M)
// get houses and its distance to school
houses |> Array.map ( fun h -> (h, Distance.tryToSchool h) )
// get houses, filter by price and print
houses |> Array.filter (fun h -> h.Price >= 100_000M) |> Array.map printHouse
// get houses, filter by price, sort price descending and print
houses |> Array.filter (fun h -> h.Price >= 100_000M) |> Array.sortByDescending (fun h -> h.Price) |> Array.map printHouse
// get houses with price over 200000 and average
houses |> Array.filter (fun h -> h.Price >= 200_000M) |> Array.averageBy (fun h -> h.Price)

