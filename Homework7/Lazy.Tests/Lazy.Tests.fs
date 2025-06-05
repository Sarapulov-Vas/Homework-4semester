module Lazy.Tests

open System.Threading
open System.Threading.Tasks
open NUnit.Framework
open FsUnit

[<Test>]
let ``Test SingleThreadLazy`` () =
    let mutable counter = 0
    let lazyValue = (SingleThreadLazy(fun () -> 
        counter <- counter + 1
        1) :> ILazy<int>)

    lazyValue.Get() |> should equal 1
    lazyValue.Get() |> should equal 1
    counter |> should equal 1

[<Test>]
let ``Test MultiThreadLazy`` () =
    let mutable counter = 0
    let lazyValue = (MultiThreadLazy(fun () -> 
        Thread.Sleep(100)
        Interlocked.Increment(&counter) |> ignore
        1) :> ILazy<int>)
    
    let results = Array.zeroCreate 10
    Parallel.For(0, 10, fun i ->
        results.[i] <- lazyValue.Get()
    ) |> ignore

    results |> Array.forall ((=) 1) |> should be True
    counter |> should equal 1

[<Test>]
let ``LockFreeLazy should return consistent value despite multiple computations`` () =
    let mutable counter = 0
    let lazyValue = (LockFreeLazy(fun () -> 
        Interlocked.Increment(&counter) |> ignore
        Thread.Sleep(10)
        1) :> ILazy<int>)
    
    let results = Array.zeroCreate 10
    Parallel.For(0, 10, fun i ->
        results.[i] <- lazyValue.Get()
    ) |> ignore
    
    results |> Array.forall ((=) 1) |> should be True
    counter |> should be (greaterThanOrEqualTo 1)

[<Test>]
let ``All implementations should handle null values`` () =
    let singleLazy = SingleThreadLazy(fun () -> null) :> ILazy<string>
    let multiLazy = MultiThreadLazy(fun () -> null) :> ILazy<string>
    let lockFreeLazy = LockFreeLazy(fun () -> null) :> ILazy<string>
    
    singleLazy.Get() |> should be Null
    multiLazy.Get() |> should be Null
    lockFreeLazy.Get() |> should be Null
