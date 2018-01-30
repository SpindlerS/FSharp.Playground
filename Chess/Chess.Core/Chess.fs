namespace Chess.Core

open System
//open FSharpx


module Position =
   
    // Pieces
    type Color = W | B
    type PieceType = King|Queen|Bishop|Knight|Rook|Pawn
    
    type Piece = 
        | White of PieceType 
        | Black of PieceType
        member x.Notation() =
            match x with
            | White Pawn    -> "P"
            | White Knight  -> "N"
            | White Bishop  -> "B"
            | White Rook    -> "R" 
            | White Queen   -> "Q"
            | White King    -> "K"
            | Black Pawn    -> "p"
            | Black Knight  -> "n"
            | Black Bishop  -> "b"
            | Black Rook    -> "r" 
            | Black Queen   -> "q"
            | Black King    -> "k"
    
    let Notation (piece : Piece option) =  
            match piece with
            | Some p -> p.Notation()
            | None -> "." 
    
    // Castling                
    type CastlingType = KingSide | QueenSide
    type Castling = 
        | WhiteCastle of CastlingType
        | BlackCastle of CastlingType
        member x.Notation() =
            match x with
            | WhiteCastle KingSide    -> "K"
            | WhiteCastle QueenSide   -> "Q"
            | BlackCastle KingSide    -> "k"
            | BlackCastle QueenSide   -> "q"
   
    // Position of a Piece
    type Column = Col of int
        with member x.Notation() = 
                match x with 
                | Col c -> (List.item (c-1) ['a'..'h']).ToString()
    type Row = Row of int
        with member x.Notation() = 
                match x with 
                | Row r -> r.ToString()
              
    type OnBoardPosition = OnBoard of Column * Row
        with member x.Notation() = 
                match x with 
                | OnBoard (c, r) -> c.Notation() + r.Notation()
 
    type OffBoardPosition = OffBoard of int

    type PiecePosition = OnBoardPosition | OffBoardPosition

    // Helper functions for notation

    /// Count the dots in a string.           
    let countDots (s:string) = (s |> String.filter (fun x -> x = '.') |> String.length |> fun x -> x.ToString())

    let (|Prefix|_|) (p:string) (s:string) =
        if s.StartsWith(p) then
            Some(s.Substring(p.Length))
        else
            None

    /// Replace consecutive occurences of dots by its number.
    let rec dots2countH (t:string) (r:string) (s:string) : string = 
        match t, s with 
        | "", ""    -> r
        | t, ""     -> r + countDots t
        | t, (Prefix "." rest) -> dots2countH (t+".") r rest
        | "", _     -> dots2countH "" (r + s.Substring(0,1)) (s.Substring(1)) 
        | t, _      -> dots2countH "" (r + (countDots t) + s.Substring(0,1)) (s.Substring(1)) 
    
    let dots2count s = dots2countH "" "" s

    type Position = 
        {
            PiecePlacement  : Map<OnBoardPosition,Piece>
            ActiveColor     : Color
            CastleOptions   : Castling list
            EnPassentTarget : OnBoardPosition option
            HalfmoveClock   : int
            FullMoveNo      : int
        }  
        member x.Notation() =
            let n = 8
            let pp = [for r in [n .. -1 .. 1] do 
                              yield [for c in [1 .. n] do 
                                     yield Notation(x.PiecePlacement.TryFind(OnBoard (Col c, Row r)))] 
                                     |> String.concat ""] 
                              |> String.concat "/" |> dots2count
            let ac = x.ActiveColor.ToString().ToLowerInvariant()
            let castleOptions = 
                [WhiteCastle KingSide; WhiteCastle QueenSide; 
                BlackCastle KingSide; BlackCastle QueenSide]            
            let co = match x.CastleOptions with  
                        | [] -> "-"
                        | _ -> castleOptions
                            |> Seq.filter (fun o -> Seq.contains o x.CastleOptions)
                            |> Seq.map (fun (c:Castling) -> c.Notation()) 
                            |> Seq.fold (+) ""
            let ep = match x.EnPassentTarget with 
                        | None -> "-"
                        | Some p -> p.Notation()
            let hm = x.HalfmoveClock.ToString()
            let fm = x.FullMoveNo.ToString()
            let result = [pp;ac;co;ep;hm;fm] 
            result |> String.concat " "

    // initial placement of pieces
    let initialPiecePlacement =
        let fs = [(Rook, 1); (Knight, 2); (Bishop, 3); 
                  (Queen, 4); (King, 5); 
                  (Bishop, 6); (Knight, 7); (Rook, 8)]
        [[1..8] |> List.map (fun x -> OnBoard (Col x, Row 2), White Pawn);
         [1..8] |> List.map (fun x -> OnBoard (Col x, Row 7), Black Pawn);
         fs     |> List.map (fun (f,c) -> OnBoard (Col c, Row 1), White f);
         fs     |> List.map (fun (f,c) -> OnBoard (Col c, Row 8), Black f)]
        |> List.concat
        |> Map.ofList

    // initial castling possibilities
    let castlingOptions = [WhiteCastle KingSide; WhiteCastle QueenSide; BlackCastle KingSide; BlackCastle QueenSide]

    // initial Position
    let initialPosition = 
        {
            PiecePlacement  = initialPiecePlacement;
            ActiveColor     = W;
            CastleOptions   = castlingOptions; 
            HalfmoveClock   = 0;
            EnPassentTarget = None;
            FullMoveNo      = 1
        }
    
module Move =

    open Position

    // Results
    type Draw = 
        | StaleMate
        | OfferDraw              //TODO
        | ThreefoldRepetition
        | FiftyMoves
        | InsufficientMaterial
    type Lose = 
        | CheckMate
        | Resignation
        | Forfeit
        | OutOfTime
    type Ending = 
        | Lose of Lose
        | Draw of Draw
        | Win
    
    type SimpleMove = 
    | Move of Piece * OnBoardPosition * OnBoardPosition
    | Check of Piece * OnBoardPosition * OnBoardPosition
    | Castling of CastlingType
    | EnPassant of Piece * OnBoardPosition
    | Promotion of Piece * OnBoardPosition * PieceType
    | Ending of Ending
    
    type WhiteMove = WhiteMove of SimpleMove
    type BlackMove = BlackMove of SimpleMove

    type Move = WhiteMove * BlackMove


module Game =

    open Position
    open Move

    type Game = Move list
    type GameState = Position

    type Board = OnBoardPosition list list 
   
    // example game (Anderssen -  Kieseritzki, London 1851)
    let immortalGame: Game =
        [
        (WhiteMove (Move (White Pawn, OnBoard (Col 5, Row 2), OnBoard (Col 5, Row 4))),
         BlackMove (Move (Black Pawn, OnBoard (Col 5, Row 7), OnBoard (Col 5, Row 5))));
        //...
        (WhiteMove (Check (White Bishop, OnBoard (Col 4, Row 6), OnBoard (Col 5, Row 7))),
         BlackMove (Ending (Lose CheckMate)))
        //(WhiteMove (Ending (Lose Resignation)), BlackMove (Ending Win))              
        ]
        
    // MakeMove: GameState -> Move -> GameState

    type MakeBoard = int -> Board
    let MakeBoard n = 
        [for x in [1 .. n] do 
         yield [for y in [1 .. n] do 
                yield OnBoard (Col y, Row x)]]
    

    let show board (f : int*int -> string) =
        let cols = 
            board 
            |> List.map (fun a -> a |> List.map(fun a -> match a with | (OnBoard (Col x, _)) -> x))
            |> List.concat  
            |> List.distinct
        let rows = 
            board 
            |> List.map (fun a -> a |> List.map(fun a -> match a with | (OnBoard (_, Row y)) -> y))  
            |> List.concat
            |> List.distinct
        let b = 
            [for r in rows |> List.rev do
                yield [for c in cols do yield f(r,c)]]
        b 
        |> List.map (String.concat " ") 
        |> (String.concat "\n") 
        |> fun x -> "\n" + x + "\n"
    
