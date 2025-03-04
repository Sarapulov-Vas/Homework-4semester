namespace TreeMap

module Tree =
    type Tree<'a> =
        | Node of 'a * Tree<'a> * Tree<'a>
        | Empty

    /// Map for binary tree
    let rec map f binTree =
        match binTree with
        | Empty -> Empty
        | Node(value, left, right) ->
            Node (f value, map f left, map f right)

