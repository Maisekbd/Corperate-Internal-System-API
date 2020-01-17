"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CouncilType_model_1 = require("./CouncilType.model");
var Decision = /** @class */ (function () {
    function Decision(id, subject, decisionNum, decisiontxt, decPath, maincatId, mainCat, SubCategoryId, SubCategory, CountryId, Country, DecisionType, DecisionTypeId, ConferenceYear, ConferenceIndex, KeyWords, AgendaItemId, AgendaItem, AgendaDetailId, AgendaDetail, DecisionAnnexPath, DecisionStatus, SuggestedExecutionDate, ReferenceItems, Companies, DecisionExecutions, CouncilTypeId, CouncilType, Departments, ExecutionNotes, ExecutionDate, fileAtta, IsExecutable, SelectedCompaniesIds, keyWordList) {
        this.Id = id;
        this.Subject = subject;
        this.DecisionNumber = decisionNum;
        this.DecisionText = decisiontxt;
        this.DecisionPath = decPath;
        this.MainCategoryId = maincatId;
        this.MainCategory = mainCat;
        this.SubCategoryId = SubCategoryId;
        this.SubCategory = SubCategory;
        this.CountryId = CountryId;
        this.Country = Country;
        this.DecisionType = DecisionType;
        this.DecisionTypeId = DecisionTypeId;
        this.ConferenceYear = ConferenceYear;
        this.ConferenceIndex = ConferenceIndex;
        this.KeyWords = KeyWords;
        this.AgendaItemId = AgendaItemId;
        this.AgendaItem = AgendaItem;
        this.AgendaDetailId = AgendaDetailId;
        this.AgendaDetail = AgendaDetail;
        this.DecisionAnnexPath = DecisionAnnexPath;
        this.DecisionStatus = DecisionStatus;
        this.SuggestedExecutionDate = SuggestedExecutionDate;
        this.ReferenceItems = ReferenceItems;
        this.Companies = Companies;
        this.DecisionExecutions = DecisionExecutions;
        this.CouncilTypeId = CouncilTypeId;
        this.CouncilType = CouncilType;
        this.Departments = Departments;
        this.ExecutionNotes = ExecutionNotes;
        this.ExecutionDate = ExecutionDate;
        this.fileAtta = fileAtta;
        this.IsExecutable = IsExecutable;
        this.SelectedCompaniesIds = SelectedCompaniesIds;
        this.KeyWordList = keyWordList;
    }
    Decision.deserialize = function (res) {
        return new Decision(res.Id, res.Subject, res.DecisionNumber, res.DecisionText, res.DecisionPath, res.MainCategoryId, res.MainCategory, res.SubCategoryId, res.SubCategory, res.CountryId, res.Country, res.DecisionType, res.DecisionTypeId, res.ConferenceYear, res.ConferenceIndex, res.KeyWords, res.AgendaItemId, res.AgendaItem, res.AgendaDetailId, res.AgendaDetail, res.DecisionAnnexPath, res.DecisionStatus, res.SuggestedExecutionDate, res.ReferenceItems, res.Companies, res.DecisionExecutions, res.CouncilTypeId, CouncilType_model_1.CouncilType.deserialize(res.CouncilType), res.Departments, res.ExecutionNotes, res.ExecutionDate, res.fileAtta, res.IsExecutable, res.SelectedCompaniesIds, res.KeyWordList);
    };
    Decision.getDecisionDefault = function () {
        return new Decision(0, "", "", "", "", 0, null, 0, null, 0, null, null, 0, 0, 0, "", 0, null, 0, null, "", 0, null, null, null, null, 0, null, null, "", null, null, false, [], []);
    };
    return Decision;
}());
exports.Decision = Decision;
//# sourceMappingURL=decision.model.js.map