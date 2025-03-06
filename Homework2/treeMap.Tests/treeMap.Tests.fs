module treeMap.Tests

open NUnit.Framework
open Homework2.Tree
open FsUnit

[<Test>]
let ``Tree map test`` () =
    let tree = 
        Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf))
    let expectedTree = 
        Node(1, Node(4, Leaf, Leaf), Node(9, Leaf, Leaf))
    let newTree = map (fun(x) -> x * x) tree
    newTree |> should equal expectedTree

let ``Tree map test with empty tree`` () =
    let tree = Leaf
    let expectedTree = Leaf
    let newTree = map (fun(x) -> x * x) tree
    newTree |> should equal expectedTree
