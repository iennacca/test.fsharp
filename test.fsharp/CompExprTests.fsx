// Thinking functionally series (bind)
// https://fsharpforfunandprofit.com/posts/computation-expressions-continuations/

open System


// What we want to avoid (the tower of doom)
let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top/bottom)

let divideByWorkflowDoom x y w z =
    let a = x |> divideBy y
    match a with
    | None -> None  // give up
    | Some a' ->    // keep going
        let b = a' |> divideBy w
        match b with
        | None -> None  // give up
        | Some b' ->    // keep going
            let c = b' |> divideBy z
            match c with
            | None -> None  // give up
            | Some c' ->    // keep going
                //return
                Some c'

// what computation expressions can do
// computation expression
type MaybeBuilder() =
    member this.Bind(m,f) = Option.bind f m
    member this.Return(x) = Some x

let maybe = new MaybeBuilder()

let divideByWorkflowCompExpr x y w z = 
    maybe {
        let! a = x |> divideBy y
        let! b = a |> divideBy w
        let! c = b |> divideBy z
        return c
    }


// another example that doesn't use Option.bind
type DbResult<'a> =
    | Success of 'a
    | Error of string

type DbResultBuilder() =
    member this.Bind(m,f) = 
        match m with
        | Error _ -> m
        | Success s -> 
            printfn "\tSuccess: %s" s 
            s |> f

    member this.Return(x) =
        printfn "Success <%A>" x
        Success x

    member this.ReturnFrom(x) = 
        printfn "Return option %A" x
        x

// let (>>=) m f = DBResultBuilder().Bind (f m) 

let getCustomerId name =
    if (name = "")
    then Error "getCustomerId failed"
    else Success "Cust42"

let getLastOrderForCustomer custId =
    if (custId = "")
    then Error "getLastOrderForCustomer failed"
    else Success "Order123"

let getLastProductForOrder orderId =
    if (orderId  = "")
    then Error "getLastProductForOrder failed"
    else Success "Product456"

let dbresult = new DbResultBuilder()

let getCustomerProduct name = 
    dbresult {
        let! custid = name |> getCustomerId 
        let! orderid = custid |> getLastOrderForCustomer
        let! productid = orderid |> getLastProductForOrder
        return productid
    }

let (>>=) m f =
    match m with
    | Error _ -> m
    | Success s -> 
        printfn "\tSuccess: %s" s 
        s |> f
        
"Cust42" |> getCustomerId >>= getLastOrderForCustomer >>= getLastProductForOrder

let getCustomerProductAsDbResult name = 
    dbresult {
        let! custid = name |> getCustomerId 
        let! orderid = custid |> getLastOrderForCustomer
        let! productid = orderid |> getLastProductForOrder
        return! Success productid
    }
