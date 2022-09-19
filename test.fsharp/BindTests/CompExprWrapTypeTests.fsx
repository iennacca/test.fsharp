// Thinking functionally (wrapper types)
// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types/


/// TOPIC: an example that doesn't use Option.bind
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

// infix bind operator: approach 1 
let (>>=) m f = dbresult.Bind (m,f) 

// infix bind operator: approach 2 
let (>>>=) m f =
    match m with
    | Error _ -> m
    | Success s -> 
        printfn "\tSuccess: %s" s 
        s |> f
        
let getCustomerProductAsDbResult name = 
    dbresult {
        let! custid = name |> getCustomerId 
        let! orderid = custid |> getLastOrderForCustomer
        let! productid = orderid |> getLastProductForOrder
        return! Success productid
    }

"Cust42" |> getCustomerId >>= getLastOrderForCustomer >>= getLastProductForOrder
