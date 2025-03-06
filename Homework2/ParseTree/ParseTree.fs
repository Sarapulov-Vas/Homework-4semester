namespace Homework2

module ParseTree =
    type Tree<'a> =
        | Operand of 'a
        | Operator of ('a -> 'a -> 'a) * Tree<'a> * Tree<'a>

    /// Map for binary tree
    let rec calculate parseTree =
        match parseTree with
        | Operand(value) -> value
        | Operator(operator, left, right) ->
            operator calculate left calculate right
