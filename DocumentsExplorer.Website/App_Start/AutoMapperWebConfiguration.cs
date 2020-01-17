using AutoMapper;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentsExplorer.Website.App_Start
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<DecisionDTO, Decision>();
                cfg.CreateMap<Decision, DecisionDTO>()
                 .ForMember(m => m.DecisionStatus, opt => opt.MapFrom(x => (EnumDecisionStatus)(int)x.DecisionStatus))
                 .ForMember(m=>m.CouncilTypeDescription, opt=>opt.MapFrom(x=>x.SubCategory.MainCategory.CouncilType.Description))
                 .ForMember(m => m.MainCategoryDescription, opt => opt.MapFrom(x => x.SubCategory.MainCategory.Description))
                 .ForMember(m => m.CountryName, opt => opt.MapFrom(x => x.Country.Name));

                cfg.CreateMap<CouncilTypeDTO, CouncilType>();
                cfg.CreateMap<CouncilType, CouncilTypeDTO>();

                cfg.CreateMap<MainCategoryDTO, MainCategory>();
                cfg.CreateMap<MainCategory, MainCategoryDTO>();

                cfg.CreateMap<SubCategoryDTO, SubCategory>();
                cfg.CreateMap<SubCategory, SubCategoryDTO>();

                cfg.CreateMap<DepartmentResponsibleDTO, DepartmentResponsible>()
                .ForMember(m=>m.Department, opt=>opt.Ignore());
                cfg.CreateMap<DepartmentResponsible, DepartmentResponsibleDTO>();

                cfg.CreateMap<CountryDTO, Country>();
                cfg.CreateMap<Country, CountryDTO>();

                cfg.CreateMap<CompanyDTO, Company>();
                cfg.CreateMap<Company, CompanyDTO>();

                //cfg.CreateMap<ContributorDTO, Contributor>();
                //cfg.CreateMap<Contributor, ContributorDTO>();

                cfg.CreateMap<ActivitySectorDTO, ActivitySector>();
                cfg.CreateMap<ActivitySector, ActivitySectorDTO>();

                cfg.CreateMap<CouncilMemberDTO, CouncilMember>();
                cfg.CreateMap<CouncilMember, CouncilMemberDTO>();

                cfg.CreateMap<RoundDTO, Round>();
                cfg.CreateMap<Round, RoundDTO>();

                cfg.CreateMap<SubCategoryDTO, SubCategory>();
                cfg.CreateMap<SubCategory, SubCategoryDTO>();

                cfg.CreateMap<DecisionTypeDTO, DecisionType>();
                cfg.CreateMap<DecisionType, DecisionTypeDTO>();

                cfg.CreateMap<DecisionExecutionDTO, DecisionExecution>();
                cfg.CreateMap<DecisionExecution, DecisionExecutionDTO>();

                cfg.CreateMap<DepartmentDTO, Department>();
                cfg.CreateMap<Department, DepartmentDTO>();

                cfg.CreateMap<MinutesOfMeetingDTO, Meeting>();
                cfg.CreateMap<Meeting, MinutesOfMeetingDTO>();

                cfg.CreateMap<MeetingAttendanceDTO, MeetingAttendance>();
                cfg.CreateMap<MeetingAttendance, MeetingAttendanceDTO>();
            });
        }
    }
}