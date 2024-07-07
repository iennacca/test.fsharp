namespace GrokTests.Domain
    open System
    open GrokTests.Core

    type SSN private (ssn: string) =  
        let _ssn = ssn

        static member Create (ssn: string) =
            // if Text.RegularExpressions.Regex.IsMatch(ssn,@"^(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$")
            //     then Ok (SSN(ssn))
            //     else Error ["SSN entry is invalid."]
            match ssn with
            | "" -> Error ["SSN must not be empty."]
            | _ -> Ok (SSN(ssn))

        member this.ToString() = _ssn


    type Person private (firstName: string, lastName: string, birthdate: string, ssn: SSN) = 
        let _firstname = firstName
        let _lastname = lastName
        let _birthdate = Convert.ToDateTime(birthdate)
        let _ssn = ssn

        static member Create (firstname:string, lastname:string, birthdate:string, ssn:string) = 
            let validateName name = 
                match name with
                | null -> Error ["Name must not be null."]
                | "" -> Error ["Name must not be empty."]
                | _ -> Ok name

            result {
                let! s1 = validateName firstname
                and! s2 = validateName lastname 
                and! s3 = SSN.Create(ssn)
                return Person(s1, s2, birthdate, s3)
            }

        member this.FullName() = _firstname + " " + _lastname

        member this.ToString() =
            $"Person:
                FirstName: {_firstname}
                LastName: {_lastname}
                SSN: {_ssn.ToString()}"

