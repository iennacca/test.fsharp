namespace test.fsharp
open System

module messageIO = 
    let slowConsoleWrite msg = 
        msg |> String.iter (fun ch->
            System.Threading.Thread.Sleep(10)
            System.Console.Write ch
            )

    type UnserializedLogger() = 
        // interface
        member this.Log msg = slowConsoleWrite msg

    type SerializedLogger() = 
        // create the mailbox processor
        let agent = MailboxProcessor.Start(fun inbox -> 
            // the message processing function
            let rec messageLoop () = async{
                // read a message
                let! msg = inbox.Receive()
                // write it to the log
                slowConsoleWrite msg
                // loop to top
                return! messageLoop ()
            }
            // start the loop
            messageLoop ()
        )
        // public interface
        member this.Log msg = agent.Post msg

