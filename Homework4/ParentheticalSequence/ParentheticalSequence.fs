namespace ParentheticalSequence

open System.Collections.Generic

module Sequence =
    /// The function checking the correctness of a parenthesized sequence.
    let check str =
        let stack = Stack<char>()
        let rec checkValid str = 
            match Seq.tryHead str with
                | Some('(') | Some('{') | Some('[') ->
                    stack.Push(Seq.head str)
                    checkValid (Seq.tail str)
                | Some(')') | Some('}') | Some(']') ->
                    if stack.Count > 0 && abs (int (Seq.head str) - int (stack.Pop())) <= 2 then
                        checkValid (Seq.tail str)
                    else 
                        false
                | None -> stack.Count = 0
                | _ -> checkValid (Seq.tail str)
        checkValid str
