using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;

namespace Ipstatuschecker.Services
{
    public class ServiceUser(IQueryIpStatusRepository<User> qeuryIpStatusRepository,
    ICommandIpStatusRepository<User> commandIpStatusRepository) : Iservices<UserDto>
    {
        public async Task<bool> AddNewUser(UserDto userDto)
{
    var user = new User
    {
        Id = userDto.Id,
        Name = userDto.Name,
        IpStatuses = userDto.IpStatuses?.Select(ip => new IpStatus
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status,
        
        }).ToList(),
        Devices = userDto.Devices?.Select(device => new Device
        {
            Id = device.Id,
            DeviceNames = device.DeviceNames,
    
        }).ToList()
    };

    try
    {
        await commandIpStatusRepository.CreateUser(user);
        return true;
    }
    catch (Exception)
    {
        return false;
    }
}
    public async Task<UserDto> UpdateNewUser(UserDto entity)
{
   
    var existingUser = await qeuryIpStatusRepository.GetByIdAsync(entity.Id );
    
    if (existingUser == null)
    {
        throw new Exception("User not found."); 
    }

    
    existingUser.Name = entity.Name;
    
    
    if (entity.IpStatuses != null)
    {
        
        existingUser.IpStatuses.Clear(); 
        existingUser.IpStatuses.AddRange(entity.IpStatuses.Select(ip => new IpStatus
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status,
          
        }));
    }

    if (entity.Devices != null)
    {
        existingUser.Devices.Clear(); 
        existingUser.Devices.AddRange(entity.Devices.Select(device => new Device
        {
            Id = device.Id,
            DeviceNames = device.DeviceNames,
         
        }));
    }

    
    await commandIpStatusRepository.UpdateUser(existingUser);

   
    return new UserDto
    {
        Id = existingUser.Id,
        Name = existingUser.Name,
        IpStatuses = existingUser.IpStatuses.Select(ip => new IpStatusDto
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status
        }).ToList(),
        Devices = existingUser.Devices.Select(device => new DeviceDto
        {
            Id = device.Id,
            DeviceNames = device.DeviceNames
        }).ToList()
    };
}
    public async Task<bool> DelteUserById(int entityId)
{
    try
    {
        bool isDeleted = await commandIpStatusRepository.DelteUser(entityId);
        if (!isDeleted)
        {
            throw new Exception("User could not be deleted."); 
        }
        return true; 
    }
    catch (Exception ex)
    {
       
        throw new Exception("An error occurred while deleting the user.", ex);
    }
}


 public async Task<List<UserDto>> GetAllUsers()
{
    try
    {
        var users = await qeuryIpStatusRepository.GetAll();
        
        var userDtos = users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
            {
                Id = ip.Id,
                IpAddress = ip.IpAddress,
                Status = ip.Status
            }).ToList(),
            Devices = user.Devices?.Select(device => new DeviceDto
            {
                Id = device.Id,
                DeviceNames = device.DeviceNames
            }).ToList()
        }).ToList();
        



        //    foreach (var userDto in userDtos)
        // {
        //     Console.WriteLine($"Id: {userDto.Id}, Name: {userDto.Name}");
        //     if (userDto.IpStatuses != null)
        //     {
        //         foreach (var ipStatus in userDto.IpStatuses)
        //         {
        //             Console.WriteLine($"  IP Status - Id: {ipStatus.Id}, IP Address: {ipStatus.IpAddress}, Status: {ipStatus.Status}");
        //         }
        //     }
        //     if (userDto.Devices != null)
        //     {
        //         foreach (var device in userDto.Devices)
        //         {
        //             Console.WriteLine($"  Device - Id: {device.Id}, Names: {string.Join(", ", device.DeviceNames)}");
        //         }
        //     }
        // }
        return userDtos;
    }
    catch (Exception ex)
    {
        
        throw new ApplicationException("An error occurred while retrieving users.", ex);
    }
}




    public async Task<UserDto> GetByUserIdAsync(int id)
{
   
    var user = await qeuryIpStatusRepository.GetByIdAsync(id);

   
    if (user == null)
    {
        throw new Exception("User not found."); 
    }

   
    var userDto = new UserDto
    {
        Id = user.Id,
        Name = user.Name,
        IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status
        }).ToList(),
        Devices = user.Devices?.Select(device => new DeviceDto
        {
            Id = device.Id,
            DeviceNames = device.DeviceNames
        }).ToList()
    };

    return userDto; 
}
    public async Task<UserDto> GetByUserNameAsync(string name)
        {
           var user = await qeuryIpStatusRepository.GetByNameAsync(name);

   
    if (user == null)
    {
        throw new Exception("User not found."); 
    }

    
    var userDto = new UserDto
    {
        Id = user.Id,
        Name = user.Name,
        IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status
        }).ToList(),
        Devices = user.Devices?.Select(device => new DeviceDto
        {
            Id = device.Id,
            DeviceNames = device.DeviceNames
        }).ToList()
    };

    return userDto; 
        }

       
    }
}