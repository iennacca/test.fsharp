#load "piglatin.fs"
open test.fsharp
PigLatin.toPigLatin "test"

#load "quicksort.fs"
open test.fsharp
printfn "%A" (Sorter.quicksort [1;5;23;18;9;1;3]);;
printfn "%A" (Sorter.quicksort2 ["test";"hello";"maniac";"vermin"])

#load "download.fs"
open test.fsharp.WebPageDownloader
let google = fetchUrl myCallback "http://google.com"

