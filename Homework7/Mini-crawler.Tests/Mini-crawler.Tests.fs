module MiniCrawler.Tests

open NUnit.Framework
open FsUnit
open Crawler

[<Test>]
let ``Test extractLinks``() =
    let page = """
        <html>
            <a href="http://site.ru/1">Link 1</a>
            <a href="https://site.ru/2">Link 2</a>
            <a href="/relative/link">Not matched</a>
            <a href="http://site.ru/3" class="test">Link 3</a>
        </html>
    """

    let result = extractLinks page
    
    result |> should equal [
        "http://site.ru/1"
        "https://site.ru/2"
        "http://site.ru/3"
    ]

[<Test>]
let ``Test crawlPage``() =
    let result = crawlPage "https://se.math.spbu.ru/" |> Async.RunSynchronously
    result.Length |> should equal 8

[<Test>]
let ``Test downloadPage``() =
    let result = downloadPage "https://se.math.spbu.ru/" |> Async.RunSynchronously
    result.Length |> should equal 42543
