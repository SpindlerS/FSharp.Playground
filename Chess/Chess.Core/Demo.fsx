#load "Chess.fs"

open System
open Chess.Core.Position
open Chess.Core.Move
open Chess.Core.Game

let c1 = Col 3
c1.Notation()
let r1 = Row 3
r1.Notation()  

let p1 = OnBoard (c1,r1)        
p1.Notation()

let d1 = countDots "..abc..de"

match "test" with 
| Prefix "te" t -> t
| _ -> ""

dots2countH "" "" "abc"   
dots2countH "" "" "...a..b.c....d....."

dots2count "...a..b.c....d....."
dots2count "abc/d.e..f/...g/h.../..i....j...k.."

initialPiecePlacement.TryFind(OnBoard(Col 1, Row 1))
initialPiecePlacement.TryFind(OnBoard(Col 1, Row 2))
initialPiecePlacement.TryFind(OnBoard(Col 5, Row 1))
initialPiecePlacement.TryFind(OnBoard(Col 5, Row 8))
initialPiecePlacement

let po = OnBoard (Col 1, Row 2)
let pi : Piece = White King
pi.ToString()
pi.Notation()

initialPosition.Notation()

Notation (Some (Black King))  
Notation None 
initialPiecePlacement.TryFind(OnBoard (Col 5, Row 8))
Notation(initialPiecePlacement.TryFind(OnBoard (Col 5, Row 1)))

List.item 1 ['a'..'h']
let chessBoard = MakeBoard 8
show chessBoard (fun (r,c) -> (List.item (c-1) ['a'..'h']).ToString()+""+r.ToString())
show chessBoard (fun (r,c) -> Notation(initialPiecePlacement.TryFind(OnBoard (Col c, Row r))))

