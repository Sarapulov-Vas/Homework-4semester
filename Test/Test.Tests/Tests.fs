module Test.Tests

open NUnit.Framework
open Test
open FsUnit

[<Test>]
let ``Infinite sequence tests`` () =
    firstSeq |> Seq.take 5 |> should equal [1; -1; 1; -1; 1]
    secondSeq |> Seq.take 5 |> should equal [1; -2; 3; -4; 5]

[<Test>]
let ``Binary tree filter test`` () =
    let tree = Node (1, Node (2, Leaf, Node(3, Leaf, Leaf)), Node (4, Leaf, Leaf))
    filter (fun x -> x % 2 = 0) tree |> should equal [2; 4]

[<Test>]
let ``Empty binary tree filter test`` () =
    let tree = Leaf
    filter (fun x -> x % 2 = 0) tree |> should be Empty

// [<Test>]
let ``Empty priority queue test`` () =
    let queue = PriorityQueue<int>()
    shouldFail (fun() -> queue.Dequeue |> ignore)

[<Test>]
let ``Priority queue test`` () =
    let queue = PriorityQueue<int>()
    queue.Enqueue(1, 2)
    queue.Enqueue(2, 3)
    queue.Enqueue(3, 1)
    queue.Dequeue |> should equal 2
    queue.Dequeue |> should equal 1
    queue.Dequeue |> should equal 3
