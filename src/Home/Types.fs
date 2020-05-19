module Home.Types

type Model = 
    {
        Messages: string[]
        MustContain: Set<string>
        MustContainTemp: string
        MustNotContain: Set<string>
        MustNotContainTemp: string
        SelectedText: (int * int * string) option
        FiltersHidden: bool
    }

type Msg =
    | FileRead of string[]
    | FileSelected of obj
    | UpdateMustContainTemp of string
    | UpdateMustNotContainTemp of string
    | AddMustContainFilter of string
    | RemoveMustContainFilter of string
    | AddMustNotContainFilter of string
    | RemoveMustNotContainFilter of string
    | ToggleFilters
