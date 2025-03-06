namespace Homework2

module ParseTree =
    type Tree<'a> =
        | Operand of 'a
        | Operator of ('a -> 'a -> 'a) * Tree<'a> * Tree<'a>

    /// Function that calculates the value of an arithmetic expression using the parse tree.
    let rec calculate parseTree =
        match parseTree with
        | Operand(value) -> value
        | Operator(operator, left, right) ->
            operator (calculate left) (calculate right)
