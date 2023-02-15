// Loosely follows the "Why use F#" series
// https://fsharpforfunandprofit.com/posts/why-use-fsharp-intro/


#if !FAKE
    #r "netstandard" // windows
    #r "Facades/netstandard" // mono
#endif

// TEST: open function: open single module (PigLatin)
#load "piglatin.fs"
open test.fsharp
PigLatin.toPigLatin "test"


// TEST: open function: open single module (Sorter)
#load "quicksort.fs"
open test.fsharp
printfn "%A" (Sorter.quicksort [1;5;23;18;9;1;3]);;
printfn "%A" (Sorter.quicksort2 ["test";"hello";"maniac";"vermin"])


// TEST: open function: open single module (WebPageDownloader)
#load "download.fs"
open test.fsharp.WebPageDownloader
// let google = fetchUrl myCallback "http://google.com"


// TEST: open function: open 3 modules, open a specific module (WebPageDownloader)
#load "piglatin.fs"
#load "quicksort.fs"
#load "download.fs"
open test.fsharp.WebPageDownloader
let google = fetchUrl myCallback "http://google.com"


// TEST:  polymorphism
#load "shapes.fs"
open test.fsharp.Shapes
let circle = Circle(10)
let rect = Rectangle(4,5)
let polygon = Polygon( [(1,1); (2,2); (3,3)])
let point = Point(2,3)
[circle; rect; polygon; point] |> List.iter draw


// TEST: using discriminated unions
type EmailAddress = EmailAddress of string
type CreationResult<'T> = Success of 'T | Error of string            
let CreateEmailAddress2 (s:string) = 
    if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$") 
        then Success (EmailAddress s)
        else Error "Email address must contain an @ sign"
CreateEmailAddress2 "example.com"


// TEST: option type
[1;2;3;4]  |> List.tryFind (fun x-> x = 3)  // Some 3
[1;2;3;4]  |> List.tryFind (fun x-> x = 10) // None


// TEST: using functions to extract boilerplate code
let values = [1..5]
let seed = 1
let action accumulator newValue = accumulator * newValue
values |> List.fold action seed
values |> List.reduce (*)
values |> List.reduce (+)


// TEST: strategy pattern using functions
#load "strategy.fs"
open test.fsharp.Animal

cat.MakeNoise
dog.MakeNoise
//try again a second later
dog.MakeNoise 
dog.MakeNoise


// TEST: >> function composition
let square x = x * x
let showResult x = printfn "result: %A" x; x
let squareShow = square >> showResult
let squareShowTwice = squareShow >> squareShow

square 5
squareShow 10
squareShowTwice 5

let log x = printfn "%A" x; x
let square (x:float) = x * x
let logsquare = log >> square >> log


// TEST: function lists
#load "functionList.fs"
open test.fsharp.FunctionList

let square x = x * x
square 5.0
traceOp square  5.0


// TEST: mapreduce
#load "wordcount.fs"
open test.fsharp.WordCount

["test";"test";"hello";"hello"] |> List.map wordCountMap;;

let t1 = ["test";"test";"hello";"hello"] |> List.map wordCountMap;;
let t2 = t1 |> Seq.groupBy fst |> Seq.map (fun(x,y) -> x, Seq.map snd y);;
let t3 = t2 |> Seq.map (fun(x,y) -> x, y |> Seq.sum);;


// TEST: mapreduce copy
// https://thecodedecanter.wordpress.com/2010/05/24/implementing-map-reduce-in-f-sharp/
#load "mapreduce.fs"
open test.fsharp.MapReduceExample

// Run the example...
let alice = ("Alice",["Dog";"Cat"])
let bob = ("Bob",["Cat"])
let charlie = ("Charlie",["Mouse"; "Cat"; "Dog"])
let dennis = ("Dennis",[])
 
let people = [alice;bob;charlie;dennis]
 
let results = people |> mapreduce mapfunc reducefunc
 
for result in results do
    let animal = fst result
    let count = ((snd result) |> Seq.toArray).[0]
    printfn "%s : %s" animal (count.ToString())
 
printfn "Press any key to exit."
System.Console.ReadKey() |> ignore


// TEST: message-based IO
#load "messageIO.fs"
open test.fsharp.messageIO

// test in isolation
slowConsoleWrite "abc"

// test in isolation
let makeTask logger taskId = async {
    let name = sprintf "Task%i" taskId
    for i in [1..3] do 
        let msg = sprintf "-%s:Loop%i-" name i
        logger msg 
    }

let task = makeTask slowConsoleWrite 1
Async.RunSynchronously task

// test in isolation
let unserializedLogger = UnserializedLogger()
unserializedLogger.Log "hello"

// test in isolation
let serializedLogger = SerializedLogger()
serializedLogger.Log "hello"

let unserializedExample = 
    let logger = new UnserializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

let serializedExample = 
    let logger = new SerializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore


// TEST: bind! and let!
#load "divideByExplicit.fs"
open test.fsharp
  
// test
let good1 = DivideByExplicit.divideByWorkflow 12 3 2 1
let bad1 = DivideByExplicit.divideByWorkflow 12 3 0 1
  
#load "divideByBind.fs"
open test.fsharp
  
// test
let good2 = DivideByBind.divideByWorkflow 12 3 2 1
let bad2 = DivideByBind.divideByWorkflow 12 3 0 1
  
  
// TEST: wordcount
open System.Text.RegularExpressions

let text = "This is a test. This is another test."
let textValues = 
    Regex.Matches(text.ToLowerInvariant(), @"\w+('\w+)*") 
        |> Seq.cast<Match> 
        |> Seq.countBy (fun m -> m.Value)
printfn "%A" textValues


// TEST: wordcount exercise
#load "wordcount.fs"
open test.fsharp

let sourceText = "This is a test. This is another what the heck test."
let strValues = WordCount.tokenizeText sourceText

strValues |> Seq.countBy id |> Seq.iter (fun v -> printfn "%A" v)
let mv = strValues |> WordCount.mapValues
let rv = strValues |> WordCount.reduceValues


// TEST: int value operations
let intValues = {1..10}


// TEST: Seq.map vs Seq.iter??
intValues |> Seq.map WordCount.printValue
intValues |> Seq.iter WordCount.printValue

intValues |> WordCount.printValue
intValues |> Seq.toList |> List.map WordCount.printValue

for s in strValues do
  printfn "%A" s


// [QUIZ]: convert number list to list of number such that
// each element ni is the product of all other elements save for itself

// folding (precludes short-circuiting the loop)
let values = [1..5]
let seed = 1
let action accumulator newValue = accumulator * newValue
values |> List.fold action seed
values |> List.reduce (*)
values |> List.reduce (+)

// INFO: switching to record type, but still investigate how to 
// pattern match tuples in discriminated unions

type NormalType = { Index: int; Product: int }
type OneZeroType = { Index: int; Product: int; ZeroIndex: int }
type MoreZerosType = MoreZeros

type ProductType = 
    | Normal of NormalType
    | OneZero of OneZeroType
    | MoreZeros

let getProductNormal acc head =
    match acc with  
    | Normal { Index = i; Product = p } ->
        Normal { Index = i + 1; Product = p * head }
    | OneZero { Index = i; Product = p; ZeroIndex = zi } ->
        OneZero { Index = i + 1; Product = p * head; ZeroIndex = zi }
    | MoreZeros -> 
        MoreZeros 

let getProductZero acc =
    match acc with  
    | Normal { Index = i; Product = p} ->
        OneZero { Index = i + 1; Product = p; ZeroIndex = i + 1 }
    | OneZero { Index = i; Product = p; ZeroIndex = zi } ->
        MoreZeros
    | MoreZeros -> 
        MoreZeros

let rec getProduct acc l = 
    match l with
    | [] -> 
        acc
    | head::tail ->
        let newacc = 
            if head <> 0 then
                getProductNormal acc head
            else
                getProductZero acc
        getProduct newacc tail 

let convertToProducts l =
    let acc = Normal { Index = 0; Product = 1 }
    let pp = getProduct acc l

    match pp with
    | Normal { Index = i; Product = p } ->
        List.map (fun(i) -> (p / i)) l
    | OneZero { Index = i; Product = p; ZeroIndex = zi } ->
        []
    | MoreZeros -> 
        []

let l1 = [1;2;3;4;5] 
let l2 = [1;3;5;0;7;9]
let l3 = [1;3;5;0;9;12;0;15;17]

l1 |> convertToProducts
l2 |> convertToProducts
l3 |> convertToProducts