namespace MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

module Crawler =
    let httpClient = new HttpClient()
    let downloadPage (url: string) =
        async {
            let! response = httpClient.GetAsync(url) |> Async.AwaitTask
            let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            return content
        }

    let extractLinks page =
        let pattern = "<a\\s+[^>]*href=\"(http[^\"]*)\"[^>]*>"
        let matches = Regex.Matches(page, pattern)
        [ for i in matches do
            if i.Groups.Count > 1 then
                yield i.Groups.[1].Value ]

    let processPage url =
        async {
            let size = (Async.RunSynchronously (downloadPage(url))).Length
            return url, size
        }

    let crawlPage startUrl =
        async {
            let page = downloadPage startUrl
            let links = Async.RunSynchronously page |> extractLinks
            let! results = 
                links
                |> List.map processPage
                |> Async.Parallel
            return results |> Array.toList
        }

    let printInfo startUrl=
        for (url, size) in Async.RunSynchronously (crawlPage startUrl) do
            printfn "%s - %d" url size
