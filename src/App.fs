module App.View

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core
open Fable.Core.JsInterop
open Types
open App.State
open Global

importAll "../sass/main.sass"

open Fable.React
open Fable.React.Props

let root model dispatch = 
    let pageHtml page =
        match page with
        | Home -> Home.View.root model.Home (HomeMsg >> dispatch)

    div
        []
        [ div
            [ ]
            [ pageHtml model.CurrentPage ] ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReactBatched "elmish-app"
|> Program.run
