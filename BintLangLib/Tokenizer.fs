module Tokenizer

open System
open Tokens

exception MatchError of rest: string

let tokenMap = Map [ "@", Leaf; "(", LParen; ")", RParen; ",", Comma ]

let spaceLike: char list = [ ' '; '\t'; '\n'; '\r' ]

let (|Empty|Cons|) (s: string) =
    if s = "" then Empty else Cons(s[0], s[1..])

let (|MatchToken|_|) (input: string) : (token * string) option =
    let matchedToken =
        tokenMap.Keys |> Seq.tryFind (fun token -> input.StartsWith token)

    match matchedToken with
    | Some op -> Some(tokenMap[op], input[op.Length ..])
    | None -> None


let (|MatchSpace|_|) =
    function
    | Cons(c, rest) when List.contains c spaceLike -> Some rest
    | _ -> None

let rec matchString1: string -> (token option * string) option =
    function
    | Empty -> None
    | MatchSpace rest -> Some(None, rest)
    | MatchToken(token, rest) -> Some(Some token, rest)
    | rest -> raise (MatchError rest)

let matchString (input: string) : Result<token list, exn> =
    try
        input
        |> List.unfold matchString1
        |> List.filter Option.isSome
        |> List.map Option.get
        |> Ok
    with MatchError rest ->
        Error(MatchError rest)
