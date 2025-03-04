module treeMap.Tests

open NUnit.Framework
open TreeMap
open FsUnit

[<Test>]
let ``Tree map test`` () =
    let tree = 
        Tree.Node(1, Tree.Node(2, Tree.Empty, Tree.Empty), Tree.Node(3, Tree.Empty, Tree.Empty))
    let expectedTree = 
        Tree.Node(1, Tree.Node(4, Tree.Empty, Tree.Empty), Tree.Node(9, Tree.Empty, Tree.Empty))
    let newTree = Tree.map (fun(x) -> x * x) tree
    newTree |> should equal expectedTree

let ``Tree map test with empty tree`` () =
    let tree = Tree.Empty
    let expectedTree = Tree.Empty
    let newTree = Tree.map (fun(x) -> x * x) tree
    newTree |> should equal expectedTree
