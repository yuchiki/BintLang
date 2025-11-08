module Tests

open Xunit

[<Fact>]
let ``Ensure BintLangLib.Parse is available`` () =
    let _ = Parser.Parse
    Assert.True(true)
