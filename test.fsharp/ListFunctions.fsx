// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/lists#operators-for-working-with-lists
// TEST: List.zip
List.zip [1..10] [11..20]
[1..10] |> List.zip [11..20] 
[1..10] |> List.zip [11..20] |> List.unzip


// TEST: iter
let list1 = [1; 2; 3]
let list2 = [4; 5; 6]
List.iter (fun x -> printfn "List.iter: element is %d" x) list1
List.iteri(fun i x -> printfn "List.iteri: element %d is %d" i x) list1
List.iter2 (fun x y -> printfn "List.iter2: elements are %d %d" x y) list1 list2
List.iteri2 (fun i x y ->
                printfn "List.iteri2: element %d of list1 is %d element %d of list2 is %d"
                  i x i y)
            list1 list2


// TEST: List.map 
let list1 = [1; 2; 3]
let list2 = [4; 5; 6]

let newList = List.map (fun x -> x + 1) list1
printfn "%A" newList

let newListAddIndex = List.mapi (fun i x -> (i,x)) list1
printfn "%A" newListAddIndex

let sumList = List.map2 (fun x y -> x + y) list1 list2
printfn "%A" sumList


// TEST: List.collect
let collectList = List.collect (fun x -> [for i in 1..3 -> x * i]) list1
printfn "%A" collectList


// TEST: List.choose
let listWords = [ "and"; "Rome"; "Bob"; "apple"; "zebra" ]
let isCapitalized (string1:string) = System.Char.IsUpper string1.[0]
let results = List.choose (fun elem ->
    match elem with
    | elem when isCapitalized elem -> Some(elem + "'s")
    | _ -> None) listWords
printfn "%A" results


// TEST: List.filter
let filterValues = [1..10]
List.filter (fun x -> x % 2 = 0) filterValues


// TEST: List.fold
let foldValues = [1..10]
let acc = 0
foldValues |> List.fold (+) acc 

// TEST: List.reduce
let reduceValues = [1..10]
reduceValues |> List.reduce (+)



