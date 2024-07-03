

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
        List.map (fun(i) -> if i = 0 then p else 0) l
    | MoreZeros -> 
        List.map (fun(i) -> 0) l 

let l1 = [1;2;3;4;5] 
let l2 = [1;3;5;0;7;9]
let l3 = [1;3;5;0;9;12;0;15;17]

l1 |> convertToProducts
l2 |> convertToProducts
l3 |> convertToProducts