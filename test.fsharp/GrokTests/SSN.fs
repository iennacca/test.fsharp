module SSN
    type SSN = 
        { Number: string }
        member this.validate input =
            printfn "%s" input 
            ignore

