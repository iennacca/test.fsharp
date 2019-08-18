// test 1: open function: open single module (PigLatin)
#load "piglatin.fs"
open test.fsharp
PigLatin.toPigLatin "test"


// test 2: open function: open single module (Sorter)
#load "quicksort.fs"
open test.fsharp
printfn "%A" (Sorter.quicksort [1;5;23;18;9;1;3]);;
printfn "%A" (Sorter.quicksort2 ["test";"hello";"maniac";"vermin"])


// test 3: open function: open single module (WebPageDownloader)
#load "download.fs"
open test.fsharp.WebPageDownloader
// let google = fetchUrl myCallback "http://google.com"


// test 4: open function: open 3 modules, open a specific module (WebPageDownloader)
#load "piglatin.fs"
#load "quicksort.fs"
#load "download.fs"
open test.fsharp.WebPageDownloader
let google = fetchUrl myCallback "http://google.com"


// test 5: polymorphism
#load "shapes.fs"
open test.fsharp.Shapes
let circle = Circle(10)
let rect = Rectangle(4,5)
let polygon = Polygon( [(1,1); (2,2); (3,3)])
let point = Point(2,3)
[circle; rect; polygon; point] |> List.iter draw


// test 6: using discriminated unions
type EmailAddress = EmailAddress of string
type CreationResult<'T> = Success of 'T | Error of string            
let CreateEmailAddress2 (s:string) = 
    if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$") 
        then Success (EmailAddress s)
        else Error "Email address must contain an @ sign"
CreateEmailAddress2 "example.com"


// test 7: option type
[1;2;3;4]  |> List.tryFind (fun x-> x = 3)  // Some 3
[1;2;3;4]  |> List.tryFind (fun x-> x = 10) // None


// test 8: strategy pattern using functions
#load "strategy.fs"
open test.fsharp.Animal

cat.MakeNoise
dog.MakeNoise
//try again a second laterdog.MakeNoise 
dog.MakeNoise

