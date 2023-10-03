using API.Models;

namespace API.DataTransferObjects;

public class EducationDTO
{
    public Guid Guid { get; set; }
    public string Major { get; set; }

    public string Degree { get; set; }

    public float Gpa { get; set; }

    public Guid UniversityGuid { get; set; }

    public static explicit operator EducationDTO(Education education)
    {
        return new EducationDTO
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            UniversityGuid = education.UniversityGuid
        };
    }

    public static implicit operator Education(EducationDTO educationDTO)
    {
        return new Education
        {
            Guid = educationDTO.Guid,
            Major = educationDTO.Major,
            Degree = educationDTO.Degree,
            Gpa = educationDTO.Gpa,
            UniversityGuid = educationDTO.UniversityGuid,
            ModifiedDate = DateTime.Now
        };
    }
}
