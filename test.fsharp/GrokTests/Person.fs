namespace Domain
    open System
    
    type SSN private (ssn: string) =  
        let _ssn = ssn

        static member Create (ssn: string) =
            if Text.RegularExpressions.Regex.IsMatch(s,@"^(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$")
                then Ok (SSN(ssn))
                else Error "SSN entry is invalid."
        
        member this.ToString() = _ssn


    type Person private (firstName: string, lastName: string, birthdate: string, ssn: string) = 
        let _firstname = firstName
        let _lastname = lastName
        let _birthdate = Convert.ToDateTime(birthdate)
        let _ssn = SSN.Create(ssn)

        member this.FullName() = _firstname + " " + _lastname

        member this.ToString() =
            $"Person:
                FirstName: {_firstname}
                LastName: {_lastname}
                SSN: {_ssn.ToString()}"

        let validateName name = 
            Ok name

        static member Create (firstname:string, lastname:string, birthdate:string, ssn:string) = 
            validateName firstname |> 
            validateName lastname |> 
            
            Ok Person(firstname, lastname, birthdate, ssn)

