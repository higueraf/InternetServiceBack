using InternetServiceBack.Models;
using InternetServiceBack.Dtos.Cash;

namespace InternetServiceBack.Repository
{
    public class CashRepository
    {
        public List<CashDto> GetCashs(DatabaseInternetServiceContext contextDB)
        {
            List<CashDto> cashDtos = new List<CashDto>();
            List<Cash> cashs = null;
            try
            {
                cashs = contextDB.Cash.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var cash in cashs)
            {
                cashDtos.Add(new CashDto
                {
                    CashID = cash.CashID,
                    Description = cash.Description
                });
            }
            return cashDtos;
        }

        public CashDto CreateCash(DatabaseInternetServiceContext contextDB, CashDto cashDto)
        {
            if (cashDto == null) return null;

            Cash cash = new Cash();
            cash.Description = cashDto.Description;
            cash.CashID = Guid.NewGuid();
            try
            {
                contextDB.Cash.Add(cash);
                contextDB.SaveChanges();
                CashDto createdCashDto = GetCashById(contextDB, cash.CashID);
                return createdCashDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Cash: {ex.Message}");
                return null;
            }
        }

        public CashDto UpdateCash(DatabaseInternetServiceContext contextDB, CashDto cashDto)
        {
            var cash = contextDB.Cash.FirstOrDefault(x => x.CashID == cashDto.CashID);
            if (cash == null)
                return null;

            cash.Description= cashDto.Description;
            try
            {
                contextDB.SaveChanges();
                return cashDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Cash: {ex.Message}");
                return null;
            }
        }

        public bool DeleteCash(DatabaseInternetServiceContext contextDB, Guid cashId)
        {
            var cash = contextDB.Cash.FirstOrDefault(x => x.CashID == cashId);
            if (cash == null)
                return false;

            try
            {
                contextDB.Cash.Remove(cash);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Cash: {ex.Message}");
                return false;
            }
        }

        public Cash? FindCashByCashName(DatabaseInternetServiceContext contextDB, CashDto cashDto)
        {
            var cashFind = contextDB.Cash.SingleOrDefault(x => x.Description == cashDto.Description);
            return cashFind;
        }


        public CashDto GetCashById(DatabaseInternetServiceContext contextDB, Guid cashID)
        {
            Cash cash = contextDB.Cash.SingleOrDefault(x => x.CashID == cashID);
            CashDto cashDto = new CashDto();
            if (cash != null)
            {
                cashDto.CashID = cash.CashID;
                cashDto.Description= cash.Description;
            }
            return cashDto;
        }
    }
}
