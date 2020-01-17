using AAAID.HR.ServiceInterface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentsExplorer.BLL.Helpers;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL
{
    public class DecisionBLL : Service<Decision>, IDecisionBLL
    {
        private readonly IRepositoryAsync<Decision> _repository;
        private readonly IRepositoryAsync<ActivitySector> _activitySectorRepository;
        private readonly IRepositoryAsync<Company> _companyRepository;
        private readonly IRepositoryAsync<Department> _departmentRepository;
        private readonly IRepositoryAsync<ReferenceType> _referenceTypeRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IRepositoryAsync<DepartmentCoordinator> _departmentCoordinatorRepository;
        private readonly IRepositoryAsync<ReferenceItem> _referenceItemRepository;
        private readonly IRepositoryAsync<DecisionExecution> _decisionExecutionRepository;
        private readonly IRepositoryAsync<CouncilTypePermission> _councilTypePermissionRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly INotificationBLL _notificationBLL;

        public DecisionBLL(IUnitOfWorkAsync unitOfWork,
             IRepositoryAsync<Decision> repository,
            IEmployeeService employeeService,
            IDepartmentService departmentService,
            IRepositoryAsync<ActivitySector> activitySectorRepository,
            IRepositoryAsync<DepartmentCoordinator> departmentCoordinatorRepository,
            IRepositoryAsync<Company> companyRepository,
            IRepositoryAsync<Department> departmentRepository,
            IRepositoryAsync<ReferenceType> referenceTypeRepository,
            IRepositoryAsync<ReferenceItem> referenceItemRepository,
            IRepositoryAsync<DecisionExecution> decisionExecutionRepository,
            IRepositoryAsync<CouncilTypePermission> councilTypePermissionRepository,
            INotificationBLL notificationBLL
           ) : base(repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _departmentCoordinatorRepository = departmentCoordinatorRepository;
            _activitySectorRepository = activitySectorRepository;
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
            _referenceTypeRepository = referenceTypeRepository;
            _referenceItemRepository = referenceItemRepository;
            _decisionExecutionRepository = decisionExecutionRepository;
            _councilTypePermissionRepository = councilTypePermissionRepository;
            _notificationBLL = notificationBLL;

        }

        public int Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                List<int> refItemIds = _repository.Query().Include(c => c.ReferenceItems).SelectQueryable().Where(c => c.Id == id).FirstOrDefault().ReferenceItems.Select(c => c.Id).ToList();
                List<int> NotIds = _notificationBLL.Query().Include(c => c.Decision).SelectQueryable().Where(c => c.DecisionId == id).Select(c => c.Id).ToList();
                refItemIds.ForEach(c => _referenceItemRepository.Delete(c));
                _unitOfWork.SaveChanges();
                NotIds.ForEach(c => _notificationBLL.Delete(c));
                _repository.Delete(id);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                if (ex is DbUpdateException)
                {
                    if (!(ex as DbUpdateException).InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    {
                        ex = new Exception("Reference Item");
                    }
                }
                throw ex;
            }

            return 0;
        }

        public DecisionDTO GetById(int id, string currentUser)
        {
            try
            {
                DecisionDTO decision = new DecisionDTO();
                var ListOfDepId = _departmentCoordinatorRepository.Query().SelectQueryable().Where(c => c.UserId == currentUser).Select(c => c.DepartmentId).ToList();
                Mapper.Map<Decision, DecisionDTO>(_repository
                     .Query()
                     .Include(c => c.Country)
                     .Include(c => c.ActivitySectors)
                     .Include(c => c.Companies)
                     .Include(c => c.AgendaItem)
                     .Include(c => c.AgendaDetail)
                     .Include(c => c.Departments)
                     .Include(c => c.ReferenceItems.Select(d => d.ReferenceType))
                     .Include(c => c.ReferenceItems.Select(d => d.ReferenceDecision))
                     .Include(c => c.DecisionExecutions.Select(d => d.Attachments))
                     .Include(c => c.SubCategory.MainCategory.CouncilType)
                     .SelectQueryable()
                     .Where(c => c.Id == id).FirstOrDefault(), decision);
                decision.KeyWordList = !String.IsNullOrEmpty(decision.KeyWords) ? decision.KeyWords.Split(' ').ToList() : null;
                decision.DecisionExecutions.ToList().ForEach(c => c.NeedAction = ListOfDepId.Contains(c.DepartmentId) && c.ExecutionDate == null);
                return decision;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<DecisionDTO> GetDecisions(string currentUserID, int councilTypeId, int mainCategoryId, int subCategoryId, string txtSerach, string decisionNO, int conferenceYear, int countryId)
        {
            try
            {
                var listOfCouncilIds = _councilTypePermissionRepository.Query().SelectQueryable().Where(c => c.UserID == currentUserID).Select(c => c.CouncilTypeId).ToList();
                var deptId = _employeeService.GetByUserId(currentUserID).DepartmentId;
                var listofDeptDecisions = _decisionExecutionRepository.Query().SelectQueryable().Where(c => c.DepartmentId == deptId).Select(c => c.DecisionId).ToList();
                return _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.ActivitySectors)
                    .Include(c => c.Departments)
                    .Include(c => c.SubCategory.MainCategory.CouncilType)
                    .SelectQueryable()
                    .Where(c => ((councilTypeId == 0 || c.SubCategory.MainCategory.CouncilTypeId == councilTypeId) && (listOfCouncilIds.Contains(c.SubCategory.MainCategory.CouncilTypeId) || listofDeptDecisions.Contains(c.Id)))
                    && (mainCategoryId == 0 || c.SubCategory.MainCategoryId == mainCategoryId)
                    && (subCategoryId == 0 || c.SubCategoryId == subCategoryId)
                    && (txtSerach == "" || c.Subject.Contains(txtSerach))
                    // && (conferenceYear == 0 || c.ConferenceYear == conferenceYear) 
                    && (decisionNO == "" || c.DecisionNumber == decisionNO)
                    && (countryId == 0 || (c.Country != null && c.CountryId == countryId)))
                    .ProjectTo<DecisionDTO>().OrderByDescending(c => c.ConferenceYear);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IQueryable<DecisionDTO> GetDecisions()
        {
            try
            {
                return _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.ActivitySectors)
                    .Include(c => c.SubCategory.MainCategory.CouncilType)
                    .SelectQueryable().Select(c => new DecisionDTO()
                    {
                        Id = c.Id,
                        DecisionNumber = c.DecisionNumber,
                        Subject = c.Subject,
                        ConferenceNumber = c.ConferenceNumber,
                        ConferenceYear = c.ConferenceYear,
                        CountryName = c.Country != null ? c.Country.Name : "",
                        SubCategoryDescription = c.SubCategory.Description,
                        MainCategoryDescription = c.SubCategory.MainCategory.Description,
                        CouncilTypeDescription = c.SubCategory.MainCategory.CouncilType.Description
                    })
                    .OrderByDescending(c => c.ConferenceYear).ThenBy(c => c.DecisionNumber);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<DecisionDTO> GetLatestDecisions(string currentUserID)
        {
            try
            {
                var listOfCouncilIds = _councilTypePermissionRepository.Query().SelectQueryable().Where(c => c.UserID == currentUserID).Select(c => c.CouncilTypeId).ToList();
                var deptId = _employeeService.GetByUserId(currentUserID).DepartmentId;
                var listofDeptDecisions = _decisionExecutionRepository.Query().SelectQueryable().Where(c => c.DepartmentId == deptId).Select(c => c.DecisionId).ToList();
                return _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.ReferenceItems)
                    .Include(c => c.Departments)
                    .Include(c => c.SubCategory.MainCategory.CouncilType)
                    .SelectQueryable()
                    .Where(c => listOfCouncilIds.Contains(c.SubCategory.MainCategory.CouncilTypeId) || listofDeptDecisions.Contains(c.Id))
                    .OrderByDescending(c => c.ExecutionDate)
                    .Take(3)
                    .ProjectTo<DecisionDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<DecisionDTO> GetDelayDecisions(string currentUserID)
        {
            try
            {
                var currentDate = DateTime.Now.AddDays(2);
                var executed = (int)EnumDecisionStatus.Executed;
                var deptId = _employeeService.GetByUserId(currentUserID).DepartmentId;
                var listofDeptDecisions = _decisionExecutionRepository.Query().SelectQueryable().Where(c => c.DepartmentId == deptId).Select(c => c.DecisionId).ToList();
                return _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.ReferenceItems)
                    .Include(c => c.Departments)
                    .Include(c => c.SubCategory.MainCategory.CouncilType)
                    .SelectQueryable()
                    .Where(c => listofDeptDecisions.Contains(c.Id) && c.ExecutionDate > currentDate && c.DecisionStatus != executed)
                    .OrderByDescending(c => c.ExecutionDate)
                    .ProjectTo<DecisionDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IList<DecisionDTO> GetExecutedDecisions(int meetingId)
        {
            try
            {
                var executed = (int)EnumDecisionStatus.Executed;
                var executedlst = _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.DecisionExecutions)
                    .Include(c => c.AgendaItem.Meeting)
                    .Include(c => c.SubCategory.MainCategory.CouncilType)
                    .SelectQueryable()
                    .Where(c => c.AgendaItem.MeetingId == meetingId && c.IsExecutable)
                    .ProjectTo<DecisionDTO>()
                    .Select(c => new
                    {
                        DecisionObj = c,
                        DecisionExecutions = c.DecisionExecutions.Where(d => d.DecisionStatus == executed).ToList()
                    }).ToList();
                foreach (var x in executedlst)
                {
                    x.DecisionObj.DecisionExecutions = x.DecisionExecutions;
                }
                return executedlst.Select(c => c.DecisionObj).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<KeyValuePair<string, int>> GetDecisionsGroupedByCountry(int forYear)
        {
            try
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                var decisionLst = _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.SubCategory.MainCategory.CouncilType)
                    .SelectQueryable()
                    .Where(c => c.CountryId.HasValue && c.ConferenceYear == forYear)
                    .GroupBy(c => c.Country.Name).Select(c => new { name = c.Key, decCount = c.ToList().Count() });

                foreach (var s in decisionLst)
                    result.Add(new KeyValuePair<string, int>(s.name, s.decCount));

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<KeyValuePair<string, int>> GetDecisionsGroupedBySector(int forYear)
        {
            try
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                var sectors = _activitySectorRepository.Query().Select().ToList();
                foreach (var s in sectors)
                {
                    var decisionCount = _repository
                    .Query()
                    .Include(c => c.ActivitySectors)
                    .SelectQueryable()
                    .Where(c => c.ConferenceYear == forYear && c.ActivitySectors.Select(d => d.Id).Contains(s.Id)).ToList().Count();
                    if (decisionCount != 0)
                        result.Add(new KeyValuePair<string, int>(s.Name, decisionCount));
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<KeyValuePair<string, int>> GetLoanDecisionsByCompany(int forYear)
        {
            try
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                //var decisionLst = _repository
                //    .Query()
                //    .Include(c => c.SubCategory)
                //    .Include(c => c.Company)
                //    .SelectQueryable()
                //    .Where(c => c.SubCategory.IsLoan && c.ConferenceYear == forYear && c.Company != null)
                //    .GroupBy(c => c.Company.Name).Select(c => new { name = c.Key, decCount = c.ToList().Count() });

                //foreach (var s in decisionLst)
                //    result.Add(new KeyValuePair<string, int>(s.name, s.decCount));

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Insert(DecisionDTO objDTO, string currentuserId)
        {

            Decision decision;
            objDTO.DecisionExecutions.ToList().ForEach(c => c.DepartmentName = _departmentService.Find(c.DepartmentId).Name);
            var decId = 0;
            try
            {
                var isValid = !_repository.Query().SelectQueryable().Where(c => c.DecisionNumber == objDTO.DecisionNumber && c.ConferenceYear != 0 && c.ConferenceYear == objDTO.ConferenceYear).Any();
                if (isValid)
                {
                    _unitOfWork.BeginTransaction();
                    decision = new Decision();
                    Mapper.Map<DecisionDTO, Decision>(objDTO, decision);
                    if (objDTO.SelectedCompaniesIds != null && objDTO.SelectedCompaniesIds.Any())
                        foreach (int company in objDTO.SelectedCompaniesIds)
                            decision.Companies.Add(_companyRepository.Find(company));
                    base.Insert(decision);
                    if (_unitOfWork.SaveChanges() > 0)
                    {
                        ///decision File
                        var decisionPath = ConfigurationManager.AppSettings["DecisionPath"] + "//" + decision.Id;
                        string fileToCopy = String.Format("{0}//{1}//{2}", ConfigurationManager.AppSettings["TempDecisonPath"], currentuserId, objDTO.DecisionPath.Replace("\"", ""));
                        if (File.Exists(fileToCopy))
                        {
                            if (!Directory.Exists(decisionPath))
                                Directory.CreateDirectory(decisionPath);
                            File.Copy(fileToCopy, decisionPath + "//" + Path.GetFileName(fileToCopy));
                            File.Delete(fileToCopy);
                            decision.DecisionPath = String.Format("{0}//{1}", decision.Id, Path.GetFileName(fileToCopy));
                        }
                        decId = decision.Id;
                        _unitOfWork.SaveChanges();
                        var attachmentNotRequirdIds = _referenceTypeRepository.Query().SelectQueryable().Where(c => c.IsReferenceDecision).Select(c => c.Id).ToList();
                        objDTO.ReferenceItems.Where(c => !attachmentNotRequirdIds.Contains(c.ReferenceTypeId)).Where(c => c.Path != "").ToList().ForEach(c =>
                         {
                             decision
                             .ReferenceItems
                             .Where(d => d.RefereceItemNo == c.RefereceItemNo).FirstOrDefault()
                             .Path = CopyAttachment(
                                 currentuserId,
                                 c.Path,
                                 decision.Id,
                                 decision.ReferenceItems.Where(d => d.RefereceItemNo == c.RefereceItemNo).FirstOrDefault().Id);

                             _unitOfWork.SaveChanges();
                         });
                    }

                    foreach (DecisionExecutionDTO execution in objDTO.DecisionExecutions)
                    {
                        List<int> enumForinfoList = new List<int>() { (int)EnumActionType.Inform, (int)EnumActionType.Save, (int)EnumActionType.Review };
                        var emp = _employeeService.GetByUserId(_departmentCoordinatorRepository.Query().SelectQueryable().Where(c => c.DepartmentId == execution.DepartmentId).FirstOrDefault().UserId);
                        Notification generatedNotification;
                        if (!enumForinfoList.Contains(execution.ActionType))
                            generatedNotification = _notificationBLL.AddNotification(emp, null, EnumerationExtension.Description(EnumNotificationType.DecisionExecution), objDTO.Subject, objDTO.ExecutionDate.Value, (int)EnumNotificationType.DecisionExecution);
                        else
                            generatedNotification = _notificationBLL.AddNotification(emp, null, EnumerationExtension.Description(EnumNotificationType.DecsionForInform), objDTO.Subject, objDTO.ExecutionDate.Value, (int)EnumNotificationType.DecsionForInform);
                        generatedNotification.DecisionId = decId;
                        _unitOfWork.SaveChanges();
                        if (Mail.SendEmail(_notificationBLL.GetById(generatedNotification.Id), EnumerationExtension.Description((EnumActionType)execution.ActionType), emp.Email))
                        {
                            _notificationBLL.NotificationIsSend(generatedNotification.Id);
                            generatedNotification.DecisionId = decId;
                            _unitOfWork.SaveChanges();
                        }
                    }
                    _unitOfWork.Commit();
                    return decision.Id;
                }
                else
                    throw new ValidationException("dublicate Decision Number");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int Update(DecisionDTO objDTO, string currentuserId)
        {
            var decId = objDTO.Id;
            objDTO.DecisionExecutions.ToList().ForEach(c => c.DepartmentName = _departmentService.Find(c.DepartmentId).Name);
            Decision decision = new Decision();
            try
            {
                var isValid = !(_repository.Query().SelectQueryable().Where(c => c.DecisionNumber == objDTO.DecisionNumber && c.ConferenceYear != 0 && c.ConferenceYear == objDTO.ConferenceYear && c.Id != objDTO.Id).Any());
                if (isValid)
                {
                    _unitOfWork.BeginTransaction();
                    decision = _repository
                     .Query()
                     .SelectQueryable().Where(c => c.Id == objDTO.Id).FirstOrDefault();

                    var referenceItems = _referenceItemRepository.Query().SelectQueryable().Where(c => c.DecisionId == decision.Id).Select(c => c.Id).ToList();
                    foreach (var referenceItem in referenceItems)
                        if (!objDTO.ReferenceItems.Select(c => c.Id).ToList().Contains(referenceItem))
                            _referenceItemRepository.Delete(referenceItem);
                    _unitOfWork.SaveChanges();

                    var decisionExecutions = _decisionExecutionRepository.Query().SelectQueryable().Where(c => c.DecisionId == decision.Id).Select(c => c.Id).ToList();
                    foreach (var decisionExecution in decisionExecutions)
                        if (!objDTO.DecisionExecutions.Select(c => c.Id).ToList().Contains(decisionExecution))
                            _decisionExecutionRepository.Delete(decisionExecution);
                    _unitOfWork.SaveChanges();

                    Mapper.Map<DecisionDTO, Decision>(objDTO, decision);
                    decision.TrackingState = TrackableEntities.TrackingState.Modified;

                    foreach (var referenceItem in decision.ReferenceItems)
                    {
                        if (referenceItem.Id == 0)
                            referenceItem.TrackingState = TrackableEntities.TrackingState.Added;
                        else
                            referenceItem.TrackingState = TrackableEntities.TrackingState.Modified;
                    }

                    foreach (var decisionExecution in decision.DecisionExecutions)
                    {
                        if (decisionExecution.Id == 0)
                            decisionExecution.TrackingState = TrackableEntities.TrackingState.Added;
                        else
                            decisionExecution.TrackingState = TrackableEntities.TrackingState.Modified;
                    }

                    base.InsertOrUpdateGraph(decision);

                    if (_unitOfWork.SaveChanges() > 0)
                    {
                        ///decision File
                        var decisionPath = ConfigurationManager.AppSettings["DecisionPath"] + "//" + decision.Id;
                        string fileToCopy = String.Format("{0}//{1}//{2}", ConfigurationManager.AppSettings["TempDecisonPath"], currentuserId, objDTO.DecisionPath.Replace("\"", ""));
                        if (File.Exists(fileToCopy))
                        {
                            if (!Directory.Exists(decisionPath))
                                Directory.CreateDirectory(decisionPath);
                            File.Copy(fileToCopy, decisionPath + "//" + Path.GetFileName(fileToCopy));
                            File.Delete(fileToCopy);
                            Directory.Delete(Path.GetDirectoryName(fileToCopy));
                            decision.DecisionPath = String.Format("{0}//{1}", decision.Id, Path.GetFileName(fileToCopy));
                        }
                        decId = decision.Id;
                        _unitOfWork.SaveChanges();
                        var attachmentNotRequirdIds = _referenceTypeRepository.Query().SelectQueryable().Where(c => c.IsReferenceDecision).Select(c => c.Id).ToList();
                        objDTO.ReferenceItems.Where(c => !attachmentNotRequirdIds.Contains(c.ReferenceTypeId)).Where(c => c.Path != "").ToList().ForEach(c =>
                        {
                            decision
                            .ReferenceItems
                            .Where(d => d.RefereceItemNo == c.RefereceItemNo).FirstOrDefault()
                            .Path = CopyAttachment(
                                currentuserId,
                                c.Path,
                                decision.Id,
                                decision.ReferenceItems.Where(d => d.RefereceItemNo == c.RefereceItemNo).FirstOrDefault().Id);

                            _unitOfWork.SaveChanges();
                        });
                    }

                    foreach (DecisionExecutionDTO execution in objDTO.DecisionExecutions)
                    {
                        List<int> enumForinfoList = new List<int>() { (int)EnumActionType.Inform, (int)EnumActionType.Save, (int)EnumActionType.Review };
                        var emp = _employeeService.GetByUserId(_departmentCoordinatorRepository.Query().SelectQueryable().Where(c => c.DepartmentId == execution.DepartmentId).FirstOrDefault().UserId);
                        Notification generatedNotification;
                        if (!enumForinfoList.Contains(execution.ActionType))
                            generatedNotification = _notificationBLL.AddNotification(emp, null, EnumerationExtension.Description(EnumNotificationType.DecisionExecution), objDTO.Subject, objDTO.ExecutionDate.Value, (int)EnumNotificationType.DecisionExecution);
                        else
                            generatedNotification = _notificationBLL.AddNotification(emp, null, EnumerationExtension.Description(EnumNotificationType.DecsionForInform), objDTO.Subject, objDTO.ExecutionDate.Value, (int)EnumNotificationType.DecsionForInform);
                        generatedNotification.DecisionId = decId;
                        _unitOfWork.SaveChanges();
                        if (execution.Id == 0)
                            if (Mail.SendEmail(_notificationBLL.GetById(generatedNotification.Id), EnumerationExtension.Description((EnumActionType)execution.ActionType), emp.Email))
                            {
                                _notificationBLL.NotificationIsSend(generatedNotification.Id);
                                generatedNotification.DecisionId = decId;
                                _unitOfWork.SaveChanges();
                            }
                    }
                    _unitOfWork.Commit();
                    return decision.Id;
                }
                else
                    throw new ValidationException("dublicate Decision Number");


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteDecision(DecisionExecutionDTO decisionExecutionDTO, string currentuserId)
        {
            DecisionExecution decisionExecution = new DecisionExecution();
            try
            {
                _unitOfWork.BeginTransaction();
                decisionExecution = _decisionExecutionRepository
                 .Query()
                 .SelectQueryable().Where(c => c.Id == decisionExecutionDTO.Id).FirstOrDefault();

                decisionExecution.ExecutionDate = DateTime.Now;
                decisionExecution.ExecutionNotes = decisionExecutionDTO.ExecutionNotes;
                decisionExecution.DecisionStatus = (int)EnumDecisionStatus.Executed;

                if (_unitOfWork.SaveChanges() > 0)
                {
                    ///decision File

                    string fileToCopy = String.Format("{0}//{1}//{2}", ConfigurationManager.AppSettings["TempDecisionExecutionPath"], currentuserId, decisionExecutionDTO.AttachementName.Replace("\"", ""));
                    if (File.Exists(fileToCopy))
                    {
                        var decisionPath = ConfigurationManager.AppSettings["DecisionPath"] + "//" + decisionExecution.DecisionId + "//DecisionExecution";
                        if (!Directory.Exists(decisionPath))
                            Directory.CreateDirectory(decisionPath);
                        if (!Directory.Exists(decisionPath + "//" + decisionExecutionDTO.Id.ToString()))
                            Directory.CreateDirectory(decisionPath + "//" + decisionExecutionDTO.Id.ToString());
                        File.Copy(fileToCopy, decisionPath + "//" + decisionExecutionDTO.Id.ToString() + "//" + Path.GetFileName(fileToCopy));
                        decisionExecution.AttachementName = String.Format("{0}//{1}", decisionExecution.DecisionId + "//DecisionExecution//" + decisionExecutionDTO.Id.ToString(), Path.GetFileName(fileToCopy));
                    }
                }
                _decisionExecutionRepository.Update(decisionExecution);
                _unitOfWork.SaveChanges();
                int decisStatus = (int)EnumDecisionStatus.NotStrated;
                var listofExecutores = _decisionExecutionRepository.Query().SelectQueryable().Where(c => c.DecisionId == decisionExecution.DecisionId).ToList();
                if (listofExecutores.Where(c => c.DecisionStatus == (int)EnumDecisionStatus.Executed).Count() == listofExecutores.Count())
                    decisStatus = (int)EnumDecisionStatus.Executed;
                else if (listofExecutores.Where(c => c.DecisionStatus == (int)EnumDecisionStatus.Executed).Count() < listofExecutores.Count() && listofExecutores.Where(c => c.DecisionStatus == (int)EnumDecisionStatus.Executed).Count() > 0)
                    decisStatus = (int)EnumDecisionStatus.UnderExecution;
                var decObject = _repository.Find(decisionExecution.DecisionId);
                decObject.DecisionStatus = decisStatus;
                decObject.TrackingState = TrackableEntities.TrackingState.Modified;
                _repository.Update(decObject);
                _unitOfWork.SaveChanges();
                var notification = _notificationBLL.Query().SelectQueryable().Where(c => c.UserId == currentuserId && c.DecisionId == decisionExecution.DecisionId).FirstOrDefault();
                notification.IsOpen = true;
                notification.TrackingState = TrackableEntities.TrackingState.Modified;
                _notificationBLL.Update(notification);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return decisionExecution.Id;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CopyAttachment(string userId, string tempFileName, int decId, int refItemId)
        {
            string newFilePath = "";
            if (!Directory.Exists(ConfigurationManager.AppSettings["DecisionPath"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["DecisionPath"]);
            if (!Directory.Exists(ConfigurationManager.AppSettings["DecisionPath"] + "//" + decId))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["DecisionPath"] + "//" + decId);
            if (!Directory.Exists(ConfigurationManager.AppSettings["DecisionPath"] + "//" + decId + "//" + refItemId))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["DecisionPath"] + "//" + decId + "//" + refItemId);
            newFilePath = ConfigurationManager.AppSettings["DecisionPath"] + "//" + decId + "//" + refItemId + "//";

            foreach (string file in Directory.GetFiles(newFilePath))
            {
                File.Delete(file);
            }
            string fileToCopy = String.Format("{0}//{1}//{2}", ConfigurationManager.AppSettings["TempDecisonPath"], userId, tempFileName.Replace("\"", ""));
            if (File.Exists(fileToCopy))
            {
                File.Copy(fileToCopy, newFilePath + Path.GetFileName(fileToCopy));
                File.Delete(fileToCopy);
                Directory.Delete(Path.GetDirectoryName(fileToCopy));
                return String.Format("{0}//{1}//{2}", decId, refItemId, Path.GetFileName(fileToCopy));
            }
            else
                return "";
        }

        public int Save(DecisionDTO obj, string currentuserId)
        {
            Decision decision;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    decision = new Decision();
                    Mapper.Map<DecisionDTO, Decision>(obj, decision);
                    if (obj.SelectedSectors.Count > 0)
                        foreach (int sector in obj.SelectedSectors)
                            decision.ActivitySectors.Add(_activitySectorRepository.Find(sector));

                    if (obj.SelectedCompaniesIds.Count > 0)
                        foreach (int company in obj.SelectedCompaniesIds)
                            decision.Companies.Add(_companyRepository.Find(company));

                    if (obj.SelectedDepartmentIds.Count > 0)
                    {
                        foreach (int department in obj.SelectedDepartmentIds)
                        {
                            decision.Departments.Add(_departmentRepository.Find(department));
                            decision.DecisionExecutions.Add(new DecisionExecution()
                            {
                                CreateDate = DateTime.Now,
                                CreatedBy = currentuserId,
                                Department = _departmentRepository.Find(department),
                                DecisionStatus = 0,
                                TrackingState = TrackableEntities.TrackingState.Added
                            });
                        }
                    }

                    decision.CreateDate = DateTime.Now;
                    decision.CreatedBy = currentuserId;
                    base.Insert(decision);
                }
                else
                {
                    decision = _repository
                        .Query()
                        .Include(c => c.ActivitySectors)
                        .Include(c => c.Companies)
                        .Include(c => c.Departments)
                        .Include(c => c.DecisionExecutions)
                        .SelectQueryable().Where(c => c.Id == obj.Id).FirstOrDefault();
                    Mapper.Map<DecisionDTO, Decision>(obj, decision);
                    foreach (var s in decision.ActivitySectors)
                        decision.ActivitySectors.Remove(s);
                    foreach (var s in decision.Companies)
                        decision.Companies.Remove(s);
                    foreach (var s in decision.Departments)
                        decision.Departments.Remove(s);
                    foreach (var s in decision.DecisionExecutions)
                    {
                        if (!obj.SelectedDepartmentIds.Contains(s.DepartmentId))
                            s.TrackingState = TrackableEntities.TrackingState.Deleted;
                    }
                    _unitOfWork.SaveChanges();
                    if (obj.SelectedSectors.Count > 0)
                        foreach (int sector in obj.SelectedSectors)
                            decision.ActivitySectors.Add(_activitySectorRepository.Find(sector));


                    if (obj.SelectedCompaniesIds.Count > 0)
                        foreach (int company in obj.SelectedCompaniesIds)
                            decision.Companies.Add(_companyRepository.Find(company));

                    if (obj.SelectedDepartmentIds.Count > 0)
                        foreach (int department in obj.SelectedDepartmentIds)
                            decision.Departments.Add(_departmentRepository.Find(department));

                    if (obj.SelectedDepartmentIds.Count > 0)
                        foreach (int department in obj.SelectedDepartmentIds)
                            if (!decision.DecisionExecutions.Select(c => c.DepartmentId).Contains(department))
                            {
                                decision.DecisionExecutions.Add(new DecisionExecution()
                                {
                                    CreateDate = DateTime.Now,
                                    CreatedBy = currentuserId,
                                    Department = _departmentRepository.Find(department),
                                    DecisionStatus = 0,
                                    TrackingState = TrackableEntities.TrackingState.Added
                                });
                            }


                    decision.LastUpdateDate = DateTime.Now;
                    decision.LastUpdateBy = currentuserId;
                    base.Update(decision);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return decision.Id;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }

}
