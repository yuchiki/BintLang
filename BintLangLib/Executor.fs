module Executor

open Ast

type value =
    | Leaf
    | Branch of value * value

let rec eval: expr -> value =
    function
    | Ast.Leaf -> Leaf
    | Ast.Branch(lhs, rhs) -> Branch(eval lhs, eval rhs)
