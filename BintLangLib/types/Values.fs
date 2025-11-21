module Values

type value =
    | Leaf
    | Branch of value * value
