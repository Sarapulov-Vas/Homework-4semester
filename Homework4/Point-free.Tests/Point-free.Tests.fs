module Point_free.Tests

open Point_free.Point_free
open NUnit.Framework
open FsCheck
open FsUnit

let PointFreeTest list n= 
    func n list = func'1 n list && 
    func'1 n list = func'2 n list &&
    func'2 n list = func'3 n list

[<Test>]
let ``Point-free equal Tests`` () =
    Check.QuickThrowOnFailure PointFreeTest

[<Test>]
let ``Point-free Tests`` () =
    func'3 5 [1; 3; 5; 7] |> should equal [5; 15; 25; 35]