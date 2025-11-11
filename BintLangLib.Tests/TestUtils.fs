module TestUtils

open Xunit

let mustSucceed: Result<'a, exn> -> 'a =
    function
    | Error err -> failwith $"unexpected error: {err}"
    | Ok res -> res

let mustSucceedAs (expected: 'a) (res: Result<'a, exn>) : unit =
    Assert.Equal<'a>(expected, res |> mustSucceed)

let mustFail: Result<'a, exn> -> unit =
    function
    | Ok res -> Assert.Fail $"Expected error but got: {res}"
    | Error err -> ()
