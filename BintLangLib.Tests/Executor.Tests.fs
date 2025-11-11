module Executor.Tests

open Xunit
open TestUtils
open Ast

let parseMustSucceedAs (expected: Executor.value) : expr -> unit = eval >> Ok >> mustSucceedAs expected


[<Fact>]
let ``base cases`` () =
    Leaf |> parseMustSucceedAs value.Leaf

    Branch(Ast.Leaf, Ast.Leaf)
    |> parseMustSucceedAs (value.Branch(value.Leaf, value.Leaf))
