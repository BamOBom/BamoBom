using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebFramework.Api;

namespace BomoBana.Areas.Admin
{
    public class CoopertionDto
    {
        public CoopertionDto()
        {
            CoopertionListDto = new HashSet<CoopertionListDto>();
            CreateCoopertion = new CreateCoopertionDto();

        }
        public ICollection<CoopertionListDto> CoopertionListDto { get; set; }
        public CreateCoopertionDto CreateCoopertion { get; set; }
    }

    public class CoopertionViewModel : CoopertionDto
    {

    }
    public class CoopertionListDto : BaseDto<CoopertionListDto, Job,int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int Request { get; set; }

        public override void CustomMappings(IMappingExpression<Job, CoopertionListDto> mappingExpression)
        {
            mappingExpression.ForMember(
                   dest => dest.Request,
                   config => config.MapFrom(src => src.JobRequests.Count(e => !e.IsDeleted )));
        }
    }
    public class CreateCoopertionDto : BaseDto<CreateCoopertionDto, Job, int>
    {
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [MaxLength(200, ErrorMessage = "{0} نباید بیش از '{1}' کاراکتر باشد.")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
