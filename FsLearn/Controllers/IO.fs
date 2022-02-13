namespace FsLearn.Controllers

open FSharp.Control
open FsHttp.DslCE
open FsHttp
open System.Text.Json
open FsToolkit.ErrorHandling

module IO =

    [<Literal>]
    let URL =
        "https://europe-west1-revolut-230009.cloudfunctions.net/revolut-ios?pairs=USDGBP&pairs=GBPUSD"

    let parseJson (json: string) =
        JsonSerializer.Deserialize<{| GBPUSD: float; USDGBP: float |}>(json)

    let fetchWithInterval =
        asyncSeq {
            while true do
                do! Async.Sleep 1000
                let! request = httpAsync { GET URL }
                let response = request |> Response.toText |> parseJson
                yield response
        }

    let getExchanges =
        fetchWithInterval
        |> AsyncSeq.toObservable
