module TokenizerTests

open Xunit
open Tokens

let mustSucceed: Result<'a, exn> -> 'a =
    function
    | Error err -> failwith $"unexpected error: {err}"
    | Ok res -> res

let mustSucceedAs (expected: 'a) (res: Result<'a, exn>) : unit =
    Assert.Equal<'a>(expected, res |> mustSucceed)

let mustFail: Result<'a, exn> -> unit =
    function
    | Ok res -> Assert.Fail $"Expected error but got: {res}"
    | Error err -> Assert.Contains("$%", err.ToString())

let matchMustSucceedAs (expected: token list) (input: string) : unit =
    Tokenizer.matchString input |> mustSucceedAs expected

let matchMustFail: string -> unit = Tokenizer.matchString >> mustFail

[<Fact>]
let ``Tokenizer.matchString returns empty for empty string`` () = matchMustSucceedAs [] ""

[<Fact>]
let ``Tokenizer.matchString tokenizes simple symbols`` () =
    matchMustSucceedAs [ Leaf; LParen; RParen; Comma ] "@(),"

[<Fact>]
let ``Tokenizer.matchString tokenizes ignoring spaces`` () =
    matchMustSucceedAs [ Leaf; LParen; RParen; Comma ] "@(  ),"

[<Fact>]
let ``Tokenizer.matchString fails for invalid string`` () = matchMustFail "abc$%"
