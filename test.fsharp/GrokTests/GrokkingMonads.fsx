// Grokking Monads: Test project from 
// https://dev.to/choc13/grokking-functors-bla

#load "Person.fs"

open System
open Domain

let p1 = { 
    FirstName = "Jerry"
    LastName = "Chaves"
    BirthDate = Convert.ToDateTime("01/01/1999")
    SSN = "111-11-1111"
}

let p2 = { 
    FirstName = "Cameron"
    LastName = "Chaves"
    BirthDate = Convert.ToDateTime("09/06/2012")
    SSN = "111-11-4444"
}

let p3 = { 
    FirstName = "Karen"
    LastName = "Chaves"
    BirthDate = Convert.ToDateTime("10/06/1978")
    SSN = "111-11-3333"
}

let p4 = { 
    FirstName = "Deckard"
    LastName = "Chaves"
    BirthDate = Convert.ToDateTime("09/18/2016")
    SSN = "111-11-2222"
}


validateFirstName p1.FirstName

let persons:Persons = [ p1; p2; p3 ]

