module TokenizerTests

open Xunit
open Tokens


let mustSucceed: Result<token list, exn> -> token list =
    function
    | Error err -> failwith $"unexpected error: {err}"
    | Ok res -> res

let mustFail: Result<token list, exn> -> unit =
    function
    | Ok res -> Assert.Fail $"Expected error but got: {res}"
    | Error err -> Assert.Contains("$%", err.ToString())

[<Fact>]
let ``Tokenizer.matchString returns empty for empty string`` () =
    let input: string = ""
    let expected: token list = []

    let res = mustSucceed (Tokenizer.matchString input)
    Assert.Equal<token list>(expected, res)

[<Fact>]
let ``Tokenizer.matchString tokenizes simple symbols`` () =
    let input: string = "@(),"
    let expected: token list = [ Leaf; LParen; RParen; Comma ]

    let res = mustSucceed (Tokenizer.matchString input)
    Assert.Equal<token list>(expected, res)


[<Fact>]
let ``Tokenizer.matchString tokenizes ignoring spaces`` () =
    let input: string = "@(  ),"
    let expected: token list = [ Leaf; LParen; RParen; Comma ]

    let res = mustSucceed (Tokenizer.matchString input)
    Assert.Equal<token list>(expected, res)


[<Fact>]
let ``Tokenizer.matchString fails for invalid string`` () =
    let input: string = "abc$%"
    Tokenizer.matchString input |> mustFail
