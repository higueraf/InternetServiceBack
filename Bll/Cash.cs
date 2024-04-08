using InternetServiceBack.Dtos;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Cash;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class CashBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly CashRepository _cashRepository;

        public CashBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _cashRepository = new CashRepository();
        }

        public GenericResponseDto<List<CashDto>> GetCashs()
        {
            try
            {
                var cashs = _cashRepository.GetCashs(_context);
                return new GenericResponseDto<List<CashDto>>
                {
                    statusCode = 200,
                    data = cashs,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<CashDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<CashDto> GetCashById(Guid cashId)
        {
            try
            {
                var cash = _cashRepository.GetCashById(_context, cashId);
                if (cash != null)
                {
                    return new GenericResponseDto<CashDto>
                    {
                        statusCode = 200,
                        data = cash,
                        message = "Cash found",
                    };
                }
                else
                {
                    return new GenericResponseDto<CashDto>
                    {
                        statusCode = 404,
                        message = "Cash not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<CashDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<CashDto> CreateCash(CashDto cashDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                Cash existingCash = _cashRepository.FindCashByCashName(_context, cashDto);
                if (existingCash != null)
                {
                    return new GenericResponseDto<CashDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.RegisterCashErrorExisteCash,
                    };
                }
                var cashDtoSaved = _cashRepository.CreateCash(_context, cashDto);
                _context.Database.CommitTransaction();
                return new GenericResponseDto<CashDto>
                {
                    statusCode = 200,
                    data = cashDtoSaved,
                    message = "",
                };
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<CashDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterCashErrorEx,
                };
            }
        }
        public GenericResponseDto<CashDto> UpdateCash(CashDto cashDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                cashDto = _cashRepository.UpdateCash(_context, cashDto);
                _context.Database.CommitTransaction();
                if (cashDto==null)
                {
                    return new GenericResponseDto<CashDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "Cash not found",
                    };
                }

                return new GenericResponseDto<CashDto>
                {
                    statusCode = 200,
                    data = cashDto,
                    message = "Cash updated successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<CashDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteCash(Guid cashId)
        {
            try
            {
                var success = _cashRepository.DeleteCash(_context, cashId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "Cash not found",
                    };
                }

                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "Cash deleted successfully",
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
