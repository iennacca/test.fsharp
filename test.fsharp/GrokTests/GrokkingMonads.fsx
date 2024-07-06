// Grokking Monads: Test project from 
// https://dev.to/choc13/grokking-functors-bla


#load "Person.fs"

open Domain


let p1 = Person.Create ("Jerry", "Chaves", "01/01/1999", "111-11-1111")

let p2 = Person.Create ("Cameron", "Chaves", "09/06/2012", "333-33-3333")

let p3 = Person.Create ("Karen", "Chaves", "09/06/2012", "111-11-4444")
let p4 = Person.Create ("Deckard", "Chaves", "09/18/2016", "444-44-4444")



let persons = [ p1; p2; p3 ]

