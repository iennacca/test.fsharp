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

// test 9: function lists
#load "functionList.fs"
open test.fsharp.FunctionList

let square x = x * x
square 5.0
traceOp square  5.0

// test 10: mapreduce
#load "wordcount.fs"
open test.fsharp.WordCount

["test";"test";"hello";"hello"] |> List.map wordCountMap;;

let t1 = ["test";"test";"hello";"hello"] |> List.map wordCountMap;;
let t2 = t1 |> Seq.groupBy fst |> Seq.map (fun(x,y) -> x, Seq.map snd y);;
let t3 = t2 |> Seq.map (fun(x,y) -> x, y |> Seq.sum);;


// test 11: mapreduce copy
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


// test 12: message-based IO
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


// test 13: bind! and let!
#load "divideByExplicit.fs"
open test.fsharp

// test
let good = DivideByExplicit.divideByWorkflow 12 3 2 1
let bad = DivideByExplicit.divideByWorkflow 12 3 0 1

#load "divideByBind.fs"
open test.fsharp

// test
let good = DivideByBind.divideByWorkflow 12 3 2 1
let bad = DivideByBind.divideByWorkflow 12 3 0 1

