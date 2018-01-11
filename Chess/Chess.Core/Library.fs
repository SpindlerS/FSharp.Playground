namespace Chess.Core

module BasicTypes =
    
    type Color = 
        | Black
        | White
    
    type Position = 
        { 
            Row: int; 
            Column: int;
        }
         
    type ChessPiece = 
        | Pawn of Color * Position      // Bauer
        | Knight of Color * Position    // Springer
        | Bishop of Color * Position    // Läufer
        | Rook of Color * Position      // Turm
        | Queen of Color * Position     // Dame
        | King of Color * Position      // König
        