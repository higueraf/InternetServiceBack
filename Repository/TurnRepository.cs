using InternetServiceBack.Models;
using InternetServiceBack.Dtos.Turn;

namespace InternetServiceBack.Repository
{
    public class TurnRepository
    {
        public List<TurnDto> GetTurns(DatabaseInternetServiceContext contextDB)
        {
            List<TurnDto> turnDtos = new List<TurnDto>();
            List<Turn> turns = null;
            try
            {
                turns = contextDB.Turn.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var turn in turns)
            {
                turnDtos.Add(new TurnDto
                {
                    TurnID = turn.TurnID,
                    Description = turn.Description
                });
            }
            return turnDtos;
        }

        public TurnDto CreateTurn(DatabaseInternetServiceContext contextDB, TurnDto turnDto)
        {
            if (turnDto == null) return null;

            Turn turn = new Turn();
            turn.Description = turnDto.Description;
            turn.TurnID = Guid.NewGuid();
            try
            {
                contextDB.Turn.Add(turn);
                contextDB.SaveChanges();
                TurnDto createdTurnDto = GetTurnById(contextDB, turn.TurnID);
                return createdTurnDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Turn: {ex.Message}");
                return null;
            }
        }

        public TurnDto UpdateTurn(DatabaseInternetServiceContext contextDB, TurnDto turnDto)
        {
            var turn = contextDB.Turn.FirstOrDefault(x => x.TurnID == turnDto.TurnID);
            if (turn == null)
                return null;

            turn.Description = turnDto.Description;
            try
            {
                contextDB.SaveChanges();
                return turnDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Turn: {ex.Message}");
                return null;
            }
        }

        public bool DeleteTurn(DatabaseInternetServiceContext contextDB, Guid turnId)
        {
            var turn = contextDB.Turn.FirstOrDefault(x => x.TurnID == turnId);
            if (turn == null)
                return false;

            try
            {
                contextDB.Turn.Remove(turn);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Turn: {ex.Message}");
                return false;
            }
        }

        public Turn? FindTurnByTurnName(DatabaseInternetServiceContext contextDB, TurnDto turnDto)
        {
            var turnFind = contextDB.Turn.SingleOrDefault(x => x.Description == turnDto.Description);
            return turnFind;
        }


        public TurnDto GetTurnById(DatabaseInternetServiceContext contextDB, Guid turnID)
        {
            Turn turn = contextDB.Turn.SingleOrDefault(x => x.TurnID == turnID);
            TurnDto turnDto = new TurnDto();
            if (turn != null)
            {
                turnDto.TurnID = turn.TurnID;
                turnDto.Description = turn.Description;
            }
            return turnDto;
        }
    }
}
