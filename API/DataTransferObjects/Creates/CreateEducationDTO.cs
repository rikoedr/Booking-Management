using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DataTransferObjects.Creates;

public class CreateEducationDTO
{
    public Guid Guid { get; set; }
    public string Major { get; set; }

    public string Degree { get; set; }

    public float Gpa { get; set; }

    public Guid UniversityGuid { get; set; }

    public static implicit operator Education(CreateEducationDTO createEducationDTO)
    {
        return new Education
        {
            Guid = createEducationDTO.Guid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            Major = createEducationDTO.Degree,
            Degree = createEducationDTO.Degree,
            Gpa = createEducationDTO.Gpa,
            UniversityGuid = createEducationDTO.UniversityGuid
        };
    }
}
