/// Grokking Functors: Test project from 
/// https://dev.to/choc13/grokking-functors-bla 


#load "ResultBuilder.fs" "Person.fs"

open System
open GrokTests.Core
open GrokTests.Domain

let person = result {
    return! Person.Create ("Jerry", "Chaves", "01/01/1999", "553-91-9595")
}

