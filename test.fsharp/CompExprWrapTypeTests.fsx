// Thinking functionally (wrapper types)
// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types/


type DbResult<'a> =
    | Success of 'a
    | Error of string

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

