namespace Homework2

module Tree =
    /// Binary tree type
    type Tree<'a> =
        | Node of 'a * Tree<'a> * Tree<'a>
        | Leaf

    /// Map for binary tree
    let rec map f binTree =
        match binTree with
        | Leaf -> Leaf
        | Node(value, left, right) ->
            Node (f value, map f left, map f right)
