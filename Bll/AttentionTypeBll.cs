using InternetServiceBack.Dtos;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.AttentionType;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class AttentionTypeBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly AttentionTypeRepository _attentionTypeRepository;

        public AttentionTypeBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _attentionTypeRepository = new AttentionTypeRepository();
        }

        public GenericResponseDto<List<AttentionTypeDto>> GetAttentionTypes()
        {
            try
            {
                var attentionTypes = _attentionTypeRepository.GetAttentionTypes(_context);
                return new GenericResponseDto<List<AttentionTypeDto>>
                {
                    statusCode = 200,
                    data = attentionTypes,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<AttentionTypeDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<AttentionTypeDto> GetAttentionTypeById(Guid attentionTypeId)
        {
            try
            {
                var attentionType = _attentionTypeRepository.GetAttentionTypeById(_context, attentionTypeId);
                if (attentionType != null)
                {
                    return new GenericResponseDto<AttentionTypeDto>
                    {
                        statusCode = 200,
                        data = attentionType,
                        message = "AttentionType found",
                    };
                }
                else
                {
                    return new GenericResponseDto<AttentionTypeDto>
                    {
                        statusCode = 404,
                        message = "AttentionType not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<AttentionTypeDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<AttentionTypeDto> CreateAttentionType(AttentionTypeDto attentionTypeDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                AttentionType existingAttentionType = _attentionTypeRepository.FindAttentionTypeByAttentionTypeName(_context, attentionTypeDto);
                if (existingAttentionType != null)
                {
                    return new GenericResponseDto<AttentionTypeDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.RegisterAttentionTypeErrorExisteAttentionType,
                    };
                }
                var attentionTypeDtoSaved = _attentionTypeRepository.CreateAttentionType(_context, attentionTypeDto);
                _context.Database.CommitTransaction();
                return new GenericResponseDto<AttentionTypeDto>
                {
                    statusCode = 200,
                    data = attentionTypeDtoSaved,
                    message = "",
                };
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<AttentionTypeDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterAttentionTypeErrorEx,
                };
            }
        }
        public GenericResponseDto<AttentionTypeDto> UpdateAttentionType(AttentionTypeDto attentionTypeDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                attentionTypeDto = _attentionTypeRepository.UpdateAttentionType(_context, attentionTypeDto);
                _context.Database.CommitTransaction();
                if (attentionTypeDto==null)
                {
                    return new GenericResponseDto<AttentionTypeDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "AttentionType not found",
                    };
                }

                return new GenericResponseDto<AttentionTypeDto>
                {
                    statusCode = 200,
                    data = attentionTypeDto,
                    message = "AttentionType updated successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<AttentionTypeDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteAttentionType(Guid attentionTypeId)
        {
            try
            {
                var success = _attentionTypeRepository.DeleteAttentionType(_context, attentionTypeId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "AttentionType not found",
                    };
                }

                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "AttentionType deleted successfully",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<bool>
                {
                    statusCode = 500,
                    data = false,
                    message = ex.Message,
                };
            }
        }
    }
}
