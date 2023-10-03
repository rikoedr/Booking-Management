using API.Models;

namespace API.DataTransferObjects.Creates;

public class CreateUniversityDTO
{
    public string Code { get; set; }
    public string Name { get; set; }
    public static implicit operator University(CreateUniversityDTO createUniversityDTO)
    {
        return new University
        {
            Guid = Guid.NewGuid(),
            Code = createUniversityDTO.Code,
            Name = createUniversityDTO.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
