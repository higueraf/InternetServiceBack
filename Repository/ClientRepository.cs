using InternetServiceBack.Models;
using InternetServiceBack.Dtos.Client;

namespace InternetServiceBack.Repository
{
    public class ClientRepository
    {
        public List<ClientDto> GetClients(DatabaseInternetServiceContext contextDB)
        {
            List<ClientDto> clientDtos = new List<ClientDto>();
            List<Client> clients = null;
            try
            {
                clients = contextDB.Client.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var client in clients)
            {
                clientDtos.Add(new ClientDto
                {
                    ClientID = client.ClientID,
                    Name = client.Name,
                    LastName = client.LastName,
                    Identification = client.Identification,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    Address = client.Address,
                    ReferenceAddress = client.ReferenceAddress,
                });
            }
            return clientDtos;
        }

        public ClientDto CreateClient(DatabaseInternetServiceContext contextDB, ClientDto clientDto)
        {
            if (clientDto == null) return null;
            Client client = new Client();
            client.Name = clientDto.Name;
            client.LastName = clientDto.LastName;
            client.Identification = clientDto.Identification;
            client.Email = clientDto.Email;
            client.PhoneNumber = clientDto.PhoneNumber;
            client.Address = clientDto.Address;
            client.ReferenceAddress = clientDto.ReferenceAddress;
            try
            {
                contextDB.Client.Add(client);
                contextDB.SaveChanges();
                ClientDto createdClientDto = GetClientById(contextDB, client.ClientID);
                return createdClientDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Client: {ex.Message}");
                return null;
            }
        }

        public ClientDto UpdateClient(DatabaseInternetServiceContext contextDB, ClientDto clientDto)
        {
            var client = contextDB.Client.FirstOrDefault(x => x.ClientID == clientDto.ClientID);
            if (client == null)
                return null;
            client.Name = clientDto.Name != null ? clientDto.Name : client.Name;
            client.LastName = clientDto.LastName != null ? clientDto.LastName: client.LastName;
            client.Identification = clientDto.Identification != null ? clientDto.Identification: client.Identification;
            client.Email = clientDto.Email != null ? clientDto.Email : client.Email;
            client.PhoneNumber = clientDto.PhoneNumber != null ? clientDto.PhoneNumber : client.PhoneNumber;
            client.Address = clientDto.Address != null ? clientDto.Address : client.Address;
            client.ReferenceAddress = clientDto.ReferenceAddress != null ? clientDto.ReferenceAddress : client.ReferenceAddress;
            try
            {
                contextDB.SaveChanges();
                return clientDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Client: {ex.Message}");
                return null;
            }
        }

        public bool DeleteClient(DatabaseInternetServiceContext contextDB, Guid clientId)
        {
            var client = contextDB.Client.FirstOrDefault(x => x.ClientID == clientId);
            if (client == null)
                return false;

            try
            {
                contextDB.Client.Remove(client);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Client: {ex.Message}");
                return false;
            }
        }


        public ClientDto GetClientById(DatabaseInternetServiceContext contextDB, Guid clientID)
        {
            Client client = contextDB.Client.SingleOrDefault(x => x.ClientID == clientID);
            ClientDto clientDto = new ClientDto();
            if (client != null)
            {
                clientDto.ClientID = client.ClientID;
                clientDto.Name = client.Name;
                clientDto.LastName = client.LastName;
                clientDto.Identification = client.Identification;
                clientDto.Email = client.Email;
                clientDto.PhoneNumber = client.PhoneNumber;
                clientDto.Address = client.Address;
                clientDto.ReferenceAddress = client.ReferenceAddress;
            }
            return clientDto;
        }
    }
}
