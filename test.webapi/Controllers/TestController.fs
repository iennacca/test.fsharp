namespace test.webapi.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open test.webapi

[<ApiController>]
[<Route("[controller]")>]
type TestController (logger : ILogger<TestController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() : int[] =
        let rng = System.Random()
        logger.LogInformation "Test controller called"
        [|
            for index in 0..4 ->
                  rng.Next(-20,55)
        |]
