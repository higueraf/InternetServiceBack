using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.AttentionStatus;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class AttentionStatusBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly AttentionStatusRepository _attentionStatusRepository;

        public AttentionStatusBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _attentionStatusRepository = new AttentionStatusRepository();
        }

        public GenericResponseDto<List<AttentionStatusDto>> GetAttentionStatuss()
        {
            try
            {
                var attentionStatuss = _attentionStatusRepository.GetAttentionStatuss(_context);
                return new GenericResponseDto<List<AttentionStatusDto>>
                {
                    statusCode = 200,
                    data = attentionStatuss,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<AttentionStatusDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<AttentionStatusDto> GetAttentionStatusById(Guid attentionStatusId)
        {
            try
            {
                var attentionStatus = _attentionStatusRepository.GetAttentionStatusById(_context, attentionStatusId);
                if (attentionStatus != null)
                {
                    return new GenericResponseDto<AttentionStatusDto>
                    {
                        statusCode = 200,
                        data = attentionStatus,
                        message = "AttentionStatus found",
                    };
                }
                else
                {
                    return new GenericResponseDto<AttentionStatusDto>
                    {
                        statusCode = 404,
                        message = "AttentionStatus not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<AttentionStatusDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<AttentionStatusDto> CreateAttentionStatus(AttentionStatusDto attentionStatusDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                AttentionStatus existingAttentionStatus = _attentionStatusRepository.FindAttentionStatusByAttentionStatusName(_context, attentionStatusDto);
                if (existingAttentionStatus != null)
                {
                    return new GenericResponseDto<AttentionStatusDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.RegisterAttentionStatusErrorExisteAttentionStatus,
                    };
                }
                var attentionStatusDtoSaved = _attentionStatusRepository.CreateAttentionStatus(_context, attentionStatusDto);
                _context.Database.CommitTransaction();
                return new GenericResponseDto<AttentionStatusDto>
                {
                    statusCode = 200,
                    data = attentionStatusDtoSaved,
                    message = "",
                };
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<AttentionStatusDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterAttentionStatusErrorEx,
                };
            }
        }
        public GenericResponseDto<AttentionStatusDto> UpdateAttentionStatus(AttentionStatusDto attentionStatusDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                attentionStatusDto = _attentionStatusRepository.UpdateAttentionStatus(_context, attentionStatusDto);
                _context.Database.CommitTransaction();
                if (attentionStatusDto==null)
                {
                    return new GenericResponseDto<AttentionStatusDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "AttentionStatus not found",
                    };
                }

                return new GenericResponseDto<AttentionStatusDto>
                {
                    statusCode = 200,
                    data = attentionStatusDto,
                    message = "AttentionStatus updated successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<AttentionStatusDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteAttentionStatus(Guid attentionStatusId)
        {
            try
            {
                var success = _attentionStatusRepository.DeleteAttentionStatus(_context, attentionStatusId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "AttentionStatus not found",
                    };
                }

                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "AttentionStatus deleted successfully",
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
