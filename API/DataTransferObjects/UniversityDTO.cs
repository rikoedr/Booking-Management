using API.Models;

namespace API.DataTransferObjects;

public class UniversityDTO
{
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public static explicit operator UniversityDTO(University university)
    {
        return new UniversityDTO
        {
            Guid = university.Guid,
            Code = university.Code,
            Name = university.Name
        };
    }

    public static implicit operator University(UniversityDTO universityDTO)
    {
        return new University
        {
            Guid = universityDTO.Guid,
            Code = universityDTO.Code,
            Name = universityDTO.Name,
            ModifiedDate = DateTime.Now
        };
    }
}
