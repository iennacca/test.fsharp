/// Grokking Functors: Test project from 
/// https://dev.to/choc13/grokking-functors-bla 


#load "Person.fs"

open System
open Domain

let p = { 
    FirstName = "Jerry"
    LastName = "Chaves"
    BirthDate = Convert.ToDateTime("01/01/1999")
    SSN = "111-11-1111"
}


validateFirstName p.FirstName
