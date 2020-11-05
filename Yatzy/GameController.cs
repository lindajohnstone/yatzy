using System;
using System.Linq;

namespace Yatzy
{
    public class GameController
    {
        private IInputReader _reader;
        private IOutputWriter _writer;
        private Scorecard _player;

        public GameController(IInputReader reader, IOutputWriter writer)
        {
            _reader = reader;
            _writer = writer;
            _player = new Scorecard();
        }

        public void RunGame()
        {
            _writer.WriteLine("Welcome to Yatzy!");
            var turn = new DiceController();
            turn.RollAllDice();
            Choice choice;
            var turnCount = 1;
            while (turnCount < 3)
            {
                _writer.WriteLine(turn.ToString());
                _writer.WriteLine("Type 'Hold' to select dice to hold or 'End' to end your turn.");
                choice = _reader.GetPlayerRollChoice();
                if (choice == Choice.End) break;
                _writer.WriteLine("Please enter the numbers of the dice you which to hold separated by either a comma (,) or a space.");
                var heldDice = _reader.GetDiceToHold();
                for (int diceNum = 1; diceNum < 6; diceNum++)
                {
                    if (!heldDice.Contains(diceNum))
                    {
                        turn.RollOneDie(diceNum - 1);
                    }
                }
                turnCount++;
            }
            _writer.WriteLine(turn.ToString());
            _writer.WriteLine("Your turn is now over.");

            _writer.WriteLine("Your Scorecard:");
            // print the scorecard

            _writer.WriteLine("Please choose a category to score in.");
            // print the available categories
            var availableCategories = _player.GetAvailableCategories();
            var category = _reader.GetCategoryChoice(availableCategories);
            _player.AddScore(turn.Dice, category);


            var total = _player.GetTotalScore();
            _writer.WriteLine($"Your final score: {total}");
        }
    }
}