"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Company = /** @class */ (function () {
    function Company(id, name, desc, dateOfIncorp, countryId, country, createdDate, activeSectors, decisions, city, capital, subscribedCapital, paidUpCapital, aAAIDSharesNum, aAAIDShareValue, aAAIDPaidCapital, aAAIDRemainCapital, address, phoneNo, email, webSite, fax, companyManager, currencyId, currency, finYearStart, finYearEnd, companyEstablishDate, aAAIDShareDate, investOpportunity) {
        this.Id = id;
        this.Name = name;
        this.Description = desc;
        this.CountryId = countryId;
        this.Country = country;
        this.DateOfIncorporation = dateOfIncorp;
        this.CreateDate = createdDate;
        this.Decisions = decisions;
        this.ActivitySectors = activeSectors;
        this.City = city;
        this.Captial = capital;
        this.SubscribedCapital = subscribedCapital;
        this.PaidUpCapital = paidUpCapital;
        this.AAAIDSharesNum = aAAIDSharesNum;
        this.AAAIDShareValue = aAAIDShareValue;
        this.AAAIDPaidCapital = aAAIDPaidCapital;
        this.AAAIDRemainCapital = aAAIDRemainCapital;
        this.Address = address;
        this.PhoneNo = phoneNo;
        this.Email = email;
        this.WebSite = webSite;
        this.Fax = fax;
        this.CompanyManager = companyManager;
        this.CurrencyId = currencyId;
        this.Currency = currency;
        this.FinYearStart = finYearStart;
        this.FinYearEnd = finYearEnd;
        this.CompanyEstablishDate = companyEstablishDate;
        this.AAAIDShareDate = aAAIDShareDate;
        this.InvestOpportunity = investOpportunity;
    }
    Company.deserialize = function (typeRes) {
        return new Company(typeRes.Id, typeRes.Name, typeRes.Description, typeRes.DateOfIncorporation, typeRes.CountryId, typeRes.Country, typeRes.CreateDate, typeRes.ActivitySectors, typeRes.Decisions, typeRes.City, typeRes.Captial, typeRes.SubscribedCapital, typeRes.PaidUpCapital, typeRes.AAAIDSharesNum, typeRes.AAAIDShareValue, typeRes.AAAIDPaidCapital, typeRes.AAAIDRemainCapital, typeRes.Address, typeRes.PhoneNo, typeRes.Email, typeRes.WebSite, typeRes.Fax, typeRes.CompanyManager, typeRes.CurrencyId, typeRes.Currency, typeRes.FinYearStart, typeRes.FinYearEnd, typeRes.CompanyEstablishDate, typeRes.AAAIDShareDate, typeRes.InvestOpportunity);
    };
    Company.getDefault = function () {
        return new Company(0, "", "", null, 0, null, null, null, null, "", 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", 0, null, null, null, null, null, "");
    };
    return Company;
}());
exports.Company = Company;
//# sourceMappingURL=Company.model.js.map