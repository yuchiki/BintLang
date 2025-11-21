open System

let rec prettyPrint: Executor.value -> string =
    function
    | Executor.Leaf -> "@"
    | Executor.Branch(l, r) -> $"({prettyPrint l}, {prettyPrint r})"

type ResultBuilder() =
    member this.Bind(computation: Result<'a, 'err>, binder: 'a -> Result<'b, 'err>) : Result<'b, 'err> =
        match computation with
        | Ok a -> binder a
        | Error err -> Error err

    member this.Return(x: 'a) : Result<'a, 'err> = Ok x

    member this.Zero() = Ok()


let result = new ResultBuilder()

let handleResult: Result<unit, 'a> -> unit =
    function
    | Ok() -> ()
    | Error err -> raise (Exception(sprintf "error: %A" err))

[<EntryPoint>]
let main_ _ =
    result {
        let input = stdin.ReadToEnd()
        let! tokens = Tokenizer.matchString input
        let! ast = Parser.Parse tokens
        let! value = Executor.eval Map.empty ast
        prettyPrint value |> printfn "%s"
    }
    |> handleResult

    0
