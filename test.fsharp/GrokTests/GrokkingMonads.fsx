// Grokking Monads: Test project from 
// https://dev.to/choc13/grokking-functors-bla

#load "ResultBuilder.fs" "Person.fs"

open GrokTests.Core
open GrokTests.Domain

let persons = result {
    let! p1 = Person.Create ("Jerry", "Chaves", "01/01/1999", "553-91-9595")
    let! p2 = Person.Create ("Cameron", "Chaves", "09/06/2012", "333-33-3333")
    let! p3 = Person.Create ("Karen", "Chaves", "09/06/2012", "111-11-4444")
    let! p4 = Person.Create ("Deckard", "Chaves", "09/18/2016", "444-44-4444")

    return [p1; p2; p3; p4]
}

let personlist = 
    match persons with
    | Ok l -> l
    | Error _ -> failwith "Whoops"

let personsWithShortCiruitedErrors = result {
    printfn "p1"
    let! p1 = Person.Create ("", "Chaves", "01/01/1999", "")
    printfn "p2"
    let! p2 = Person.Create ("", "Chaves", "09/06/2012", "333-33-3333")
    return [p1; p2]
}

let personsWithCollatedErrors = result {
    printfn "p1"
    let! p1 = Person.Create ("", "Chaves", "01/01/1999", "")
    and! p2 = Person.Create ("", "Chaves", "09/06/2012", "333-33-3333")
    return [p1; p2]
}
