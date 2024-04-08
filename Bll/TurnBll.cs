using InternetServiceBack.Dtos;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Turn;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class TurnBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly TurnRepository _turnRepository;

        public TurnBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _turnRepository = new TurnRepository();
        }

        public GenericResponseDto<List<TurnDto>> GetTurns()
        {
            try
            {
                var turns = _turnRepository.GetTurns(_context);
                return new GenericResponseDto<List<TurnDto>>
                {
                    statusCode = 200,
                    data = turns,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<TurnDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<TurnDto> GetTurnById(Guid turnId)
        {
            try
            {
                var turn = _turnRepository.GetTurnById(_context, turnId);
                if (turn != null)
                {
                    return new GenericResponseDto<TurnDto>
                    {
                        statusCode = 200,
                        data = turn,
                        message = "Turn found",
                    };
                }
                else
                {
                    return new GenericResponseDto<TurnDto>
                    {
                        statusCode = 404,
                        message = "Turn not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<TurnDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<TurnDto> CreateTurn(TurnDto turnDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                Turn existingTurn = _turnRepository.FindTurnByTurnName(_context, turnDto);
                if (existingTurn != null)
                {
                    return new GenericResponseDto<TurnDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.RegisterTurnErrorExisteTurn,
                    };
                }
                var turnDtoSaved = _turnRepository.CreateTurn(_context, turnDto);
                _context.Database.CommitTransaction();
                if (turnDtoSaved != null) {
                    return new GenericResponseDto<TurnDto>
                    {
                        statusCode = 200,
                        data = turnDtoSaved,
                        message = "",
                    };
                } else 
                {
                    return new GenericResponseDto<TurnDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = "Error Creating Turn",
                    };
                }

            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<TurnDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterTurnErrorEx,
                };
            }
        }
        public GenericResponseDto<TurnDto> UpdateTurn(TurnDto turnDto)
        {
            try
            {
                turnDto = _turnRepository.UpdateTurn(_context, turnDto);
                if (turnDto == null)
                {
                    return new GenericResponseDto<TurnDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "Turn not found",
                    };
                }

                return new GenericResponseDto<TurnDto>
                {
                    statusCode = 200,
                    data = turnDto,
                    message = "Turn updated successfully",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<TurnDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteTurn(Guid turnId)
        {
            try
            {
                var success = _turnRepository.DeleteTurn(_context, turnId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "Turn not found",
                    };
                }

                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "Turn deleted successfully",
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
