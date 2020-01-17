using AutoMapper;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentsExplorer.API.App_Start
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DecisionDTO, Decision>();
                //.ForMember(m => m.ReferenceItems, opt => opt.Ignore())
                //.ForMember(m => m.DecisionExecutions, opt => opt.Ignore());
                cfg.CreateMap<Decision, DecisionDTO>()
                 .ForMember(m => m.DecisionStatus, opt => opt.MapFrom(x => (EnumDecisionStatus)(int)x.DecisionStatus))
                 .ForMember(m => m.CouncilType, opt => opt.MapFrom(x => x.SubCategory.MainCategory.CouncilType))
                 .ForMember(m => m.CouncilTypeDescription, opt => opt.MapFrom(x => x.SubCategory.MainCategory.CouncilType.Description))
                 .ForMember(m => m.CouncilTypeId, opt => opt.MapFrom(x => x.SubCategory.MainCategory.CouncilType.Id))
                 .ForMember(m => m.MainCategory, opt => opt.MapFrom(x => x.SubCategory.MainCategory))
                 .ForMember(m => m.MainCategoryDescription, opt => opt.MapFrom(x => x.SubCategory.MainCategory.Description))
                 .ForMember(m => m.MainCategoryId, opt => opt.MapFrom(x => x.SubCategory.MainCategory.Id))
                 .ForMember(m => m.SelectedCompaniesIds, opt => opt.MapFrom(x => x.Companies.Select(c => c.Id)))
                 .ForMember(m => m.Country, opt => opt.MapFrom(x => x.Country))
                 .ForMember(m => m.CountryId, opt => opt.MapFrom(x => x.Country.Id))
                 .ForMember(m => m.CountryName, opt => opt.MapFrom(x => x.Country.Name));

                cfg.CreateMap<DecisionExecutionDTO, DecisionExecution>();
                cfg.CreateMap<DecisionExecution, DecisionExecutionDTO>()
                 .ForMember(m => m.DecisionStatus, opt => opt.MapFrom(x => (EnumDecisionStatus)(int)x.DecisionStatus));

                cfg.CreateMap<CouncilTypeDTO, CouncilType>();
                cfg.CreateMap<CouncilType, CouncilTypeDTO>();

                cfg.CreateMap<MainCategoryDTO, MainCategory>();
                cfg.CreateMap<MainCategory, MainCategoryDTO>();

                cfg.CreateMap<SubCategoryDTO, SubCategory>();
                cfg.CreateMap<SubCategory, SubCategoryDTO>();

                cfg.CreateMap<DepartmentResponsibleDTO, DepartmentResponsible>()
                .ForMember(m => m.Department, opt => opt.Ignore());
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

                cfg.CreateMap<MeetingDTO, Meeting>()
                .ForMember(m => m.MeetingAttendances, opt => opt.Ignore());
                cfg.CreateMap<Meeting, MeetingDTO>();


                cfg.CreateMap<AgendaDetailDTO, AgendaDetail>();
                cfg.CreateMap<AgendaDetail, AgendaDetailDTO>();

                cfg.CreateMap<AgendaItemDTO, AgendaItem>();
                //.ForMember(m => m.AgendaDetails, opt => opt.Ignore());
                cfg.CreateMap<AgendaItem, AgendaItemDTO>();

                cfg.CreateMap<MeetingAttendanceDTO, MeetingAttendance>();
                cfg.CreateMap<MeetingAttendance, MeetingAttendanceDTO>()
                 .ForMember(m => m.MemberType, opt => opt.MapFrom(x => (EnumMemberType)(int)x.MemberType));

                cfg.CreateMap<ReferenceItemDTO, ReferenceItem>();
                cfg.CreateMap<ReferenceItem, ReferenceItemDTO>()
                .ForMember(m => m.RefDecisionPath, opt => opt.MapFrom(x => x.ReferenceDecision.DecisionPath));

                cfg.CreateMap<NotificationDTO, Notification>();
                cfg.CreateMap<Notification, NotificationDTO>();


            });
        }
    }
}