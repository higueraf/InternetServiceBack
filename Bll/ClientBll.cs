using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Client;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class ClientBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly ClientRepository _clientRepository;

        public ClientBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _clientRepository = new ClientRepository();
        }

        public GenericResponseDto<List<ClientDto>> GetClients()
        {
            try
            {
                var clients = _clientRepository.GetClients(_context);
                return new GenericResponseDto<List<ClientDto>>
                {
                    statusCode = 200,
                    data = clients,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<ClientDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<ClientDto> GetClientById(Guid clientId)
        {
            try
            {
                var client = _clientRepository.GetClientById(_context, clientId);
                if (client != null)
                {
                    return new GenericResponseDto<ClientDto>
                    {
                        statusCode = 200,
                        data = client,
                        message = "Client found",
                    };
                }
                else
                {
                    return new GenericResponseDto<ClientDto>
                    {
                        statusCode = 404,
                        message = "Client not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<ClientDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }


    public GenericResponseDto<ClientDto> CreateClient(ClientDto clientDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                var clientDtoSaved = _clientRepository.CreateClient(_context, clientDto);
                _context.Database.CommitTransaction();
                return new GenericResponseDto<ClientDto>
                {
                    statusCode = 200,
                    data = clientDtoSaved,
                    message = "",
                };
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<ClientDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterClientErrorEx,
                };
            }
        }

        public GenericResponseDto<ClientDto> UpdateClient(ClientDto clientDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                ClientDto clientUpdatedDto = _clientRepository.UpdateClient(_context, clientDto);
                _context.Database.CommitTransaction();
                if (clientUpdatedDto == null)
                {
                    return new GenericResponseDto<ClientDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "Client not found",
                    };
                }

                return new GenericResponseDto<ClientDto>
                {
                    statusCode = 200,
                    data = clientUpdatedDto,
                    message = "Client updated successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<ClientDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteClient(Guid clientId)
        {
            _context.Database.BeginTransaction();
            try
            {
                var success = _clientRepository.DeleteClient(_context, clientId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "Client not found",
                    };
                }
                _context.Database.CommitTransaction();
                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "Client deleted successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
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
