module Executor.Tests

open Xunit
open TestUtils
open Ast

let evalMustSucceedAs (expected: Executor.value) : expr -> unit =
    eval Map.empty >> mustSucceedAs expected

let evalWithEnvMustSucceedAs (env: environment) (expected: Executor.value) : expr -> unit =
    eval env >> mustSucceedAs expected

let evalMustFail: expr -> unit = eval Map.empty >> mustFail

[<Fact>]
let ``base cases`` () =
    Leaf |> evalMustSucceedAs value.Leaf

    Branch(Ast.Leaf, Ast.Leaf)
    |> evalMustSucceedAs (value.Branch(value.Leaf, value.Leaf))

    Variable "foo"
    |> evalWithEnvMustSucceedAs (Map.ofList [ ("foo", value.Leaf) ]) value.Leaf

    Variable "notExistingVar" |> evalMustFail
