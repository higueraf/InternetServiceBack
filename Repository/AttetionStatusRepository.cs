using InternetServiceBack.Models;
using InternetServiceBack.Dtos.AttentionStatus;

namespace InternetServiceBack.Repository
{
    public class AttentionStatusRepository
    {
        public List<AttentionStatusDto> GetAttentionStatuss(DatabaseInternetServiceContext contextDB)
        {
            List<AttentionStatusDto> attentionStatusDtos = new List<AttentionStatusDto>();
            List<AttentionStatus> attentionStatuses = null;
            try
            {
                attentionStatuses = contextDB.AttentionStatus.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var attentionStatus in attentionStatuses)
            {
                attentionStatusDtos.Add(new AttentionStatusDto
                {
                    AttentionStatusID = attentionStatus.AttentionStatusID,
                    Description = attentionStatus.Description
                });
            }
            return attentionStatusDtos;
        }

        public AttentionStatusDto CreateAttentionStatus(DatabaseInternetServiceContext contextDB, AttentionStatusDto attentionStatusDto)
        {
            if (attentionStatusDto == null) return null;

            AttentionStatus attentionStatus = new AttentionStatus();
            attentionStatus.Description = attentionStatusDto.Description;
            attentionStatus.AttentionStatusID = Guid.NewGuid();
            try
            {
                contextDB.AttentionStatus.Add(attentionStatus);
                contextDB.SaveChanges();
                AttentionStatusDto createdAttentionStatusDto = GetAttentionStatusById(contextDB, attentionStatus.AttentionStatusID);
                return createdAttentionStatusDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating AttentionStatus: {ex.Message}");
                return null;
            }
        }

        public AttentionStatusDto UpdateAttentionStatus(DatabaseInternetServiceContext contextDB, AttentionStatusDto attentionStatusDto)
        {
            var attentionStatus = contextDB.AttentionStatus.FirstOrDefault(x => x.AttentionStatusID == attentionStatusDto.AttentionStatusID);
            if (attentionStatus == null)
                return null;

            attentionStatus.Description= attentionStatusDto.Description;
            try
            {
                contextDB.SaveChanges();
                return attentionStatusDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating AttentionStatus: {ex.Message}");
                return null;
            }
        }

        public bool DeleteAttentionStatus(DatabaseInternetServiceContext contextDB, Guid attentionStatusId)
        {
            var attentionStatus = contextDB.AttentionStatus.FirstOrDefault(x => x.AttentionStatusID == attentionStatusId);
            if (attentionStatus == null)
                return false;

            try
            {
                contextDB.AttentionStatus.Remove(attentionStatus);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting AttentionStatus: {ex.Message}");
                return false;
            }
        }

        public AttentionStatus? FindAttentionStatusByAttentionStatusName(DatabaseInternetServiceContext contextDB, AttentionStatusDto attentionStatusDto)
        {
            var attentionStatusFind = contextDB.AttentionStatus.SingleOrDefault(x => x.Description == attentionStatusDto.Description);
            return attentionStatusFind;
        }


        public AttentionStatusDto GetAttentionStatusById(DatabaseInternetServiceContext contextDB, Guid attentionStatusID)
        {
            AttentionStatus attentionStatus = contextDB.AttentionStatus.SingleOrDefault(x => x.AttentionStatusID == attentionStatusID);
            AttentionStatusDto attentionStatusDto = new AttentionStatusDto();
            if (attentionStatus != null)
            {
                attentionStatusDto.AttentionStatusID = attentionStatus.AttentionStatusID;
                attentionStatusDto.Description= attentionStatus.Description;
            }
            return attentionStatusDto;
        }
    }
}
