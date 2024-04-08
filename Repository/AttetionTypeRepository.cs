using InternetServiceBack.Models;
using InternetServiceBack.Dtos.AttentionType;

namespace InternetServiceBack.Repository
{
    public class AttentionTypeRepository
    {
        public List<AttentionTypeDto> GetAttentionTypes(DatabaseInternetServiceContext contextDB)
        {
            List<AttentionTypeDto> attentionTypeDtos = new List<AttentionTypeDto>();
            List<AttentionType> attentionTypes = null;
            try
            {
                attentionTypes = contextDB.AttentionType.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var attentionType in attentionTypes)
            {
                attentionTypeDtos.Add(new AttentionTypeDto
                {
                    AttentionTypeID = attentionType.AttentionTypeID,
                    Description = attentionType.Description
                });
            }
            return attentionTypeDtos;
        }

        public AttentionTypeDto CreateAttentionType(DatabaseInternetServiceContext contextDB, AttentionTypeDto attentionTypeDto)
        {
            if (attentionTypeDto == null) return null;

            AttentionType attentionType = new AttentionType();
            attentionType.Description = attentionTypeDto.Description;
            attentionType.AttentionTypeID = Guid.NewGuid();
            try
            {
                contextDB.AttentionType.Add(attentionType);
                contextDB.SaveChanges();
                AttentionTypeDto createdAttentionTypeDto = GetAttentionTypeById(contextDB, attentionType.AttentionTypeID);
                return createdAttentionTypeDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating AttentionType: {ex.Message}");
                return null;
            }
        }

        public AttentionTypeDto UpdateAttentionType(DatabaseInternetServiceContext contextDB, AttentionTypeDto attentionTypeDto)
        {
            var attentionType = contextDB.AttentionType.FirstOrDefault(x => x.AttentionTypeID == attentionTypeDto.AttentionTypeID);
            if (attentionType == null)
                return null;

            attentionType.Description= attentionTypeDto.Description;
            try
            {
                contextDB.SaveChanges();
                return attentionTypeDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating AttentionType: {ex.Message}");
                return null;
            }
        }

        public bool DeleteAttentionType(DatabaseInternetServiceContext contextDB, Guid attentionTypeId)
        {
            var attentionType = contextDB.AttentionType.FirstOrDefault(x => x.AttentionTypeID == attentionTypeId);
            if (attentionType == null)
                return false;

            try
            {
                contextDB.AttentionType.Remove(attentionType);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting AttentionType: {ex.Message}");
                return false;
            }
        }

        public AttentionType? FindAttentionTypeByAttentionTypeName(DatabaseInternetServiceContext contextDB, AttentionTypeDto attentionTypeDto)
        {
            var attentionTypeFind = contextDB.AttentionType.SingleOrDefault(x => x.Description == attentionTypeDto.Description);
            return attentionTypeFind;
        }


        public AttentionTypeDto GetAttentionTypeById(DatabaseInternetServiceContext contextDB, Guid attentionTypeID)
        {
            AttentionType attentionType = contextDB.AttentionType.SingleOrDefault(x => x.AttentionTypeID == attentionTypeID);
            AttentionTypeDto attentionTypeDto = new AttentionTypeDto();
            if (attentionType != null)
            {
                attentionTypeDto.AttentionTypeID = attentionType.AttentionTypeID;
                attentionTypeDto.Description= attentionType.Description;
            }
            return attentionTypeDto;
        }
    }
}
