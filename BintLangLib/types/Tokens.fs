module Tokens

type token =
    | Leaf
    | LParen
    | RParen
    | Comma
    | Identifier of string
