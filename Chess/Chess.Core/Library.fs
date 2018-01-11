namespace Chess.Core

module BasicTypes =
    
    type Color = White | Black
    
    type Row = A|B|C|D|E|F|G|H
    type Column = 1|2|3|4|5|6|7|8
    type Position = OnBoard of Row * Column | OffBoard

    type PieceType = King|Queen|Bishop|Knight|Rook|Pawn
    type Piece = Color * PieceType * Position
    
    type Draw = 
        | StaleMate
        | OfferDraw              //TODO
        | ThreefoldRepetition
        | FiftyMoves
        | InsufficientMaterial
    type Lose = CheckMate|Resignation|Forfeit|OutOfTime
    type Ending = 
        | Lose of Lose
        | Draw of Draw
        | Win
    type CastlingType = KingSide|QueenSide
    type SimpleMove = 
    | MovePiece of Piece * Position
    | Castling of CastlingType
    | EnPassant of Piece
    | Promotion of Piece * PieceType
    | Check of Piece * Position
    | Ending
    
    type WhiteMove = WhiteMove of SimpleMove
    type BlackMove = BlackMove of SimpleMove

    type Move = WhiteMove * BlackMove
    
    type Game = Move list

    type GameState = Piece list

    // initial Board
    let initialGameState: GameState =
        {
            [White, Pawn, A, 2],
            [White, King, E, 1],
            [Black, King, E, 8] // ...
        }


    // MakeMove: (GameState * Move) -> GameState

