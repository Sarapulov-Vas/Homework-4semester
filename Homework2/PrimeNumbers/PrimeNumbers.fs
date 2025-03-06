namespace Homework2

module PrimeNumbers =
    let isPrime n =
        {2..int(sqrt(float n))} |> Seq.forall (fun x -> n % x <> 0)
    let primes =
        Seq.initInfinite (fun x -> x + 2) |> Seq.filter isPrime

