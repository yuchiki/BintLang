module Parser.Tests

open Xunit
open Tokens
open TestUtils

let parseMustSucceedAs (expected: Ast.expr) : token list -> unit = Parse >> mustSucceedAs expected

let parseMustFail: token list -> unit = Parse >> mustFail

[<Fact>]
let ``parse Leaf`` () = [ Leaf ] |> parseMustSucceedAs Ast.Leaf

[<Fact>]
let ``parse Branch`` () =
    [ LParen; Leaf; Comma; Leaf; RParen ]
    |> parseMustSucceedAs (Ast.Branch(Ast.Leaf, Ast.Leaf))

[<Fact>]
let ``parse Branch recursively`` () =
    [ LParen
      LParen
      Leaf
      Comma
      LParen
      Leaf
      Comma
      Leaf
      RParen
      RParen
      Comma
      Leaf
      RParen ]
    |> parseMustSucceedAs (Ast.Branch(Ast.Branch(Ast.Leaf, Ast.Branch(Ast.Leaf, Ast.Leaf)), Ast.Leaf))
