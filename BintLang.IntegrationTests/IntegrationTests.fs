module IntegrationTests

open Xunit
open TestingUtils

[<Fact>]
let ``test1`` () =
    run "hoge" [] |> ensureSucceed |> outputIs "input: hoge"
