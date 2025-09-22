module ParentheticalSequence.Tests

open NUnit.Framework
open FsUnit
open ParentheticalSequence.Sequence

[<TestCase("", true)>]
[<TestCase("  abc  ", true)>]
[<TestCase("()", true)>]
[<TestCase("(){}[]", true)>]
[<TestCase("({[]})", true)>]
[<TestCase("(abc){adc}[abc]", true)>]
[<TestCase("({aaa[ada]})", true)>]
[<TestCase("({)[]}", false)>]
[<TestCase("{}[}(}", false)>]
[<TestCase("([cba]{abc}", false)>]
[<TestCase("([cba]{abc}))abc", false)>]
[<TestCase("{)", false)>]
[<TestCase("(}", false)>]
let ``Test of bracket sequence check function`` str result =
    check str |> should equal result
