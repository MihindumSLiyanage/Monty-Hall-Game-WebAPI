using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly Random _random = new Random();

        public async Task<object> PlayGameAsync(MontyHallGame game)
        {
            // Randomly select the door with the prize behind it.
            int prizeDoor = _random.Next(1, 4);

            // List of all doors.
            int[] doors = { 1, 2, 3 };

            // Doors that can be revealed (excluding the initial selection and the prize door).
            int[] availableDoors = doors.Where(d => d != game.InitialDoorSelection && d != prizeDoor).ToArray();

            // Randomly select one of the available doors to be revealed.
            int revealedDoor = availableDoors[_random.Next(0, availableDoors.Length)];

            // Determine the final door selection based on the player's choice to change or not.
            int finalDoor = game.ChangeDoor
                ? doors.Single(d => d != game.InitialDoorSelection && d != revealedDoor)
                : game.InitialDoorSelection;

            // Check if the final door is the prize door.
            bool isWin = finalDoor == prizeDoor;

            // Message to be returned if the player wins.
            string message = isWin ? Messages.WinMessage : Messages.LoseMessage;

            // Construct the response object.
            var response = new
            {
                InitialDoorSelection = game.InitialDoorSelection,
                RevealedDoor = revealedDoor,
                FinalDoor = finalDoor,
                IsWin = isWin,
                CorrectDoor = prizeDoor,
                Message = message
            };

            return response;

        }
    }
}
