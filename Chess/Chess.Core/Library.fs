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
         
    // Die Figuren haben alle eine Farbe und eine Position auf dem Brett.
    // Wurde eine Figur geschlagen, hat sie keine Position mehr auf dem Brett -> deshalb option.
    // ABER: wird der K�nig geschlagen bzw. hat keine Zugm�glichkeit mehr, ist das Spiel beendet.
    type ChessPiece = 
        | Pawn of Color * Position option      // Bauer
        | Knight of Color * Position option    // Springer
        | Bishop of Color * Position option    // L�ufer
        | Rook of Color * Position option      // Turm
        | Queen of Color * Position option     // Dame
        | King of Color * Position             // K�nig

    type GameState = ChessPiece list  

module BasicFunctions =
    
    open BasicTypes
    
    let createInitialGameState: GameState =
        
        list.Empty


