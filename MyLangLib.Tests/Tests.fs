module Tests

open System
open Xunit

[<Fact>]
let ``Ensure MyLangLib.Parse is available`` () =
    let _ = Parser.Parse
    Assert.True(true)
