module Domain
    open System
    
    type SSN = 
        { Number: string }
        member this.validate input =
            printfn "%s" input 
            ignore

    type Person = 
        { 
            FirstName: string
            LastName: string
            BirthDate: DateTime
            SSN: string
        }

        static member Default = {
            FirstName = "Test"
            LastName ="Testing"
            BirthDate = Convert.ToDateTime("1/1/2000")
            SSN = "111-11-1111"
        }

        member this.FullName = this.FirstName + " " + this.LastName


    let validateFirstName firstname = 
        if  String.length firstname > 0 then
            Ok firstname
        else
            Error "First name cannot be empty"

    let validateLastName lastname = 
        if  String.length lastname > 0 then
            Ok lastname
        else
            Error "First name cannot be empty"

    let printPerson person = 
        $"Person:
            FirstName: {person.FirstName}
            LastName: {person.LastName}
            SSN: {person.SSN}"

    type Persons = Person list
