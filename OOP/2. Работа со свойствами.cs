using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Player player = new Player('@', 2, 2);
            Renderer renderer = new Renderer();

            renderer.RenderPlayer(player);
        }
        
        class Player
        {

            public Player(char playerSymbol, int positionX, int positionY)
            {
                Symbol = playerSymbol;
                PositionX = positionX;
                PositionY = positionY;
            }

            public char Symbol {  get; private set; }
            public int PositionX { get; private set; }
            public int PositionY { get; private set; }
        }

        class Renderer
        {
            public void RenderPlayer(Player player)
            {
                Console.SetCursorPosition(player.PositionX, player.PositionY);
                Console.WriteLine(player.Symbol);
            }
        }
    }
}